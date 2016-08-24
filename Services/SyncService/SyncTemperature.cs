using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.ServiceProcess;
using System.Threading;
using System.Threading.Tasks;
using Services.DownloadManager;
using Services.Temperature.Providers;

namespace Services.SyncService
{
    public partial class SyncTemperature : ServiceBase
    {
        private CancellationTokenSource _cancellation;
        private Task _syncTask;
        private TimeSpan _defaultTimeout, _maxTimeout;
        private DateTime _defaultDateTime;
        private string _httpUrl;

        public SyncTemperature()
        {
            InitializeComponent();
            CanPauseAndContinue = true;
            AutoLog = false;
            Trace.Listeners.Add(new EventLogTraceListener(nameof(SyncTemperature)));
        }

        private void SetDefaultValues()
        {
            _defaultTimeout = TimeSpan.FromMinutes(1);
            _maxTimeout = TimeSpan.FromMinutes(20);
            _defaultDateTime = new DateTime(2016, 6, 1, 0, 0, 0);
            _httpUrl = System.Configuration.ConfigurationManager.AppSettings["TemperatureHttpServer"];
        }

        protected override async void OnStart(string[] args)
        {
            Trace.TraceInformation("OnStart SyncTask");
            try
            {
                SetDefaultValues();
                _cancellation = new CancellationTokenSource();

                _syncTask = NextSync(_defaultTimeout, _cancellation.Token);
            }
            catch (Exception ex)
            {
                Trace.TraceError("Sync cannot be started. " + ex);
                Stop();
            }
        }

        private async Task NextSync(TimeSpan timeout, CancellationToken cancellationToken)
        {
            Trace.TraceInformation("NextSync started.");
            try
            {
                Trace.TraceInformation($"Before provider.");
                using (var provider = new TemperatureProvider())
                {
                    var lastDateTime = await provider.GetLastTemperatureDate();

                    DateTime requestDateTime;
                    if (lastDateTime.HasValue)
                    {
                        requestDateTime = lastDateTime.Value;
                        Trace.TraceInformation($"Last date in database is {lastDateTime}. "); 
                    }
                    else
                    {
                        requestDateTime = _defaultDateTime;
                        Trace.TraceInformation($"Last date in database is absent. Use default datetime {_defaultDateTime}");
                    }

                    requestDateTime = requestDateTime.AddMinutes(1);
                    var httpDownload = new HttpDownload(_httpUrl);
                    Trace.TraceInformation($"Send request with date {requestDateTime}. ");
                    httpDownload.SendRequest(requestDateTime);
                    httpDownload.GetResponse();

                    if (httpDownload.IsPagePresent())
                    {
                        var device = httpDownload.DownloadTemperature();
                        Trace.TraceInformation($"Device name {device.Name}. Found {device.Temperatures.Count()} temperatures information. ");
                        if (device.Temperatures.Any())
                        {
                            Trace.TraceInformation($"Start add {device.Temperatures.Count()} items temperatures information to database. ");
                            var deviceId = await provider.AddDeviceIfNotExist(device);

                            Task task = provider.AddTemperatures(deviceId, device.Temperatures);
                        }
                    }
                    else
                    {
                        Trace.TraceWarning($"Response without StatusCode - OK");
                    }

                    await Task.Delay(timeout, cancellationToken);
                }
            }
            catch (OperationCanceledException)
            {
                return;
            }
            catch (HttpRequestException ex)
            {
                if ((timeout += timeout) > _maxTimeout)
                {
                    Trace.TraceError($"Communication error, but maximum timeout was reached. Sync will be terminated. {ex}");
                    Environment.Exit(1);
                }
                else
                {
                    Trace.TraceWarning($"Communication error. Timeout increased to {timeout}. {ex}");
                    Trace.TraceInformation("Timeout " + timeout);
                    await Task.Delay(timeout, cancellationToken);
                }
            }
            finally
            { 
                Trace.TraceWarning("Before NextSync");
                await NextSync(timeout, cancellationToken);
            }
        }


        private bool StopSyncTask()
        {
            bool stopped = false;
            try
            {
                Trace.TraceWarning("StopSyncTask start");
                if (_cancellation != null)
                {
                    if (_syncTask != null)
                    {
                        _cancellation.Cancel();
                        try
                        {
                            if (_syncTask.Wait(10000))
                            {
                                _syncTask.Dispose();
                            }
                        }
                        catch (AggregateException aex)
                        {
                            Trace.TraceWarning("StopSyncTask " + aex);
                            if (aex.InnerExceptions.Any(e => e.GetType() == typeof(TaskCanceledException)))
                                _syncTask.Dispose();
                        }
                        catch (TaskCanceledException ex)
                        {
                            Trace.TraceWarning("StopSyncTask " + ex);
                            _syncTask.Dispose();
                        }
                        _syncTask = null;
                        stopped = true;
                    }
                    _cancellation.Dispose();
                    _cancellation = null;
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError("Error while stopping sync task. " + ex);
            }
            return stopped;
        }


        protected override void OnStop()
        {
            if (_syncTask == null || StopSyncTask())
            {
                Trace.TraceInformation("Sync stopped");
            }
            else
            {
                Trace.TraceError("Sync cannot be stopped. Process will be terminated.");
                Environment.Exit(1);
            }
        }
    }
}
