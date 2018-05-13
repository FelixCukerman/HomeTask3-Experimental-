using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using HomeTask3_Experimental_.Parking;

namespace HomeTask3_Experimental_.Services
{
    public class ParkingService
    {
        private Parking.Parking parking;

        public ParkingService()
        {
            parking = Parking.Parking.Create;
        }

        public async Task<string> GetFreePlace()
        {
            var count = await Task.Run(() =>
            {
                return JsonConvert.SerializeObject(parking.FreePlace());
            });

            return count;
        }

        public async Task<string> GetBookedPlace()
        {
            var count = await Task.Run(() =>
            {
                return JsonConvert.SerializeObject(parking.BookedPlace());
            });

            return count;
        }

        public async Task<string> GetCurrentProfit()
        {
            var count = await Task.Run(() =>
            {
                return JsonConvert.SerializeObject(parking.ShowProfit());
            });

            return count;
        }
    }
}