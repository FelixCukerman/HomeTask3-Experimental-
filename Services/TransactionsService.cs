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
    public class TransactionsService
    {
        private Parking.Parking parking;

        public TransactionsService()
        {
            parking = Parking.Parking.Create;
        }

        public async Task<string> GetTransaction()
        {
            var strData = await Task.Run(() =>
            {
                return JsonConvert.SerializeObject(parking.GetAllTransactions());
            });

            return strData;
        }


        public async Task<string> PutTransaction(int id, int cash)
        {
            var strData = await Task.Run(() =>
            {
                return JsonConvert.SerializeObject(parking.AddCash(id, cash));
            });

            return strData;
        }
    }
}