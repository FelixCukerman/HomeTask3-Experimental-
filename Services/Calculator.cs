using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;
using HomeTask3_Experimental_.Parking;

namespace HomeTask3_Experimental_.Services
{
    class LoadUsersService
    {
        private Parking.Parking parking;

        public LoadUsersService()
        {
            parking = Parking.Parking.Create;
        }

        public async Task<IEnumerable<Car>> GetCar()
        {
            var strData = await Task.Run(() =>
            {
                return parking.AllCar;
            });

            return strData;
        }
    }
}