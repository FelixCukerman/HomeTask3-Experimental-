using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using HomeTask3_Experimental_.Services;
using HomeTask3_Experimental_.Parking;

namespace HomeTask3_Experimental_.Controllers
{
    [Produces("application/json")]
    public class TransactionsController : Controller
    {
        private TransactionsService service {get; set;}
        public TransactionsController(TransactionsService service)
        {
            this.service = service;
        }

        // GET api/GetTransactions (example)
        [Route("api/GetTransactions")]
        [HttpGet]
        public async Task<JsonResult> GetTransactions()
        {
            var query = JsonConvert.DeserializeObject<List<Transaction>>(await service.GetTransaction());
            return Json(query);
        }

        // GET api/GetLastMinTransactions (example)
        [Route("api/GetLastMinTransactions")]
        [HttpGet]
        public async Task<JsonResult> GetLastMinTransactions()
        {
            var query = JsonConvert.DeserializeObject<List<Transaction>>(await service.GetTransaction()).Where(x => x.Date.Second >= (DateTime.Now.Second - 60));
            return Json(query);
        }

        [Route("api/GetTransactionById/{id}")]
        [HttpGet]
        public async Task<JsonResult> GetTransactionById(int id)
        {
            var query = JsonConvert.DeserializeObject<List<Transaction>>(await service.GetTransaction()).Where(x => (x.Date.Second >= (DateTime.Now.Second - 60)) || (x.Id == id));
            return Json(query);
        }

        // PUT api/Put/5
        [HttpPut("api/PutCash/{id}&{cash}")]
        public async Task<JsonResult> Put(int id, int cash)
        {
            try
            {
                await service.PutTransaction(id, cash);
                return Json(Ok());
            }
            catch
            {
                return Json(BadRequest());
            }
        }
    }
}
