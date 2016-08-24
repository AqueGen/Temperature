using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Services.Temperature.DTO;


namespace Services.Temperature.Providers
{
    public class TemperatureProvider : BaseProvider
    {
        public async Task AddTemperatures(IEnumerable<TemperatureDTO> temperatureDTOList)
        {
            Context.Temperatures.AddRange(temperatureDTOList.Select(m => new Data.Store.Models.Temperature(m)));
            await Context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TemperatureDTO>> GetTemperature(PeriodDTO temperatureDTO)
        {
            var start = temperatureDTO.Start ?? DateTime.MinValue;
            var end = temperatureDTO.End ?? DateTime.MaxValue;

            var items = await Context.Temperatures.Where(m => m.Date > start && m.Date < end).ToListAsync();

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