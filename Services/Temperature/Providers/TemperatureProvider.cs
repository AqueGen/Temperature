using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Core.Model;
using Core.Model.Interfaces;
using Data.Store.Models;
using Services.Temperature.DTO;

namespace Services.Temperature.Providers
{
    public class TemperatureProvider : BaseProvider
    {
        public async Task<int> AddDeviceIfNotExist(IDevice device)
        {
            var deviceModel = await Context.Devices.FirstOrDefaultAsync(m => m.Name == device.Name);

            if (deviceModel == null)
            {
                var model = new Device {Name = device.Name};

                Context.Devices.Add(model);
                await Context.SaveChangesAsync();
                return model.Id;
            }
            else
            {
                return deviceModel.Id;
            }
        }

        public async Task<IEnumerable<DeviceDTO>> GetDevices()
        {
            var devices = await Context.Devices.ToListAsync();

            return devices.Select(m => new DeviceDTO(m));
        }

        public async Task AddTemperatures(int devideId, IEnumerable<ITemperature> temperatures)
        {
            Context.Temperatures.AddRange(
                temperatures.Select(m => new Data.Store.Models.Temperature(m) {DeviceId = devideId}));

            await Context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ITemperature>> GetTemperature(Guid deviceGuid, DateTime? startPeriod,
            DateTime? endPeriod)
        {
            var start = startPeriod ?? DateTime.MinValue;
            var end = endPeriod ?? DateTime.MaxValue;

            var device = await Context.Devices
                .Include(m => m.Temperatures)
                .Where(m => m.Guid == deviceGuid)
                .FirstOrDefaultAsync();

            var items = device.Temperatures.Where(m => m.Date > start && m.Date < end);

            return items.Select(m => new TemperatureDTO(m));
        }

        public async Task<DateTime?> GetLastTemperatureDate()
        {
            var temperatures = Context.Temperatures;

            if (temperatures.Any())
            {
                return await temperatures.MaxAsync(m => m.Date);
            }
            else
            {
                return null;
            }
        }
    }
}