using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Services.DownloadManager.Models;
using Services.Temperature.DTO;

namespace Services.DownloadManager
{
    public class HttpDownload : IDisposable
    {
        public string Url { get; }

        private const string _dateTimeFormat = "dd-MM-yyyy HH:mm";

        private HttpWebRequest _request;
        private HttpWebResponse _response;

        public HttpDownload(string url)
        {
            Url = url;
        }


        public HttpDownload SendRequest(DateTime time)
        {
            if (time == null)
                throw new ArgumentNullException($"Argument {nameof(time)} time is null");


            _request = (HttpWebRequest) WebRequest.Create($"{Url}{time.ToString(_dateTimeFormat)}");

            _request.Proxy = WebRequest.DefaultWebProxy;
            _request.AllowAutoRedirect = false;
            return this;
        }

        public void GetResponse()
        {
            _response = (HttpWebResponse) _request.GetResponse();
        }

        public bool IsPagePresent()
        {
            return _response.StatusCode == HttpStatusCode.OK;
        }

        public DeviceDTO DownloadTemperature()
        {
            string responseResult;

            Encoding encoding = Encoding.GetEncoding(1252);

            using (StreamReader responseStream = new StreamReader(_response.GetResponseStream(), encoding))
            {
                responseResult = responseStream.ReadToEnd();
                responseStream.Close();
            }


            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                DateFormatString = _dateTimeFormat
            };

            var device = JsonConvert.DeserializeObject<DeviceJson>(responseResult, settings);
            return new DeviceDTO(device) {Temperatures = device.Temperatures};
        }

        public void Dispose()
        {
            _response.Dispose();
        }
    }
}