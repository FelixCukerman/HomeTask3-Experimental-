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
    public class ParkingController : Controller
    {
        private ParkingService service {get; set;}
        public ParkingController(ParkingService service)
        {
            this.service = service;
        }

        // GET api/GetCars (example)
        [Route("api/GetFreePlace")]
        [HttpGet]
        public async Task<JsonResult> GetFreePlace()
        {
            var query = JsonConvert.DeserializeObject<int>(await service.GetFreePlace());
            return Json(query);
        }

        [Route("api/GetBookedPlace")]
        [HttpGet]
        public async Task<JsonResult> GetBookedPlace()
        {
            var query = JsonConvert.DeserializeObject<int>(await service.GetBookedPlace());
            return Json(query);
        }

        [Route("api/GetCurrentProfit")]
        [HttpGet]
        public async Task<JsonResult> GetCurrentProfit()
        {
            var query = JsonConvert.DeserializeObject<double>(await service.GetCurrentProfit());
            return Json(query);
        }
    }
}
