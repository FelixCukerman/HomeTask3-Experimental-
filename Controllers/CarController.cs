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
    public class CarController : Controller
    {
        private LoadCarService service {get; set;}
        public CarController(LoadCarService service)
        {
            this.service = service;
        }

        // GET api/GetCars (example)
        [Route("api/GetCars")]
        [HttpGet]
        public async Task<JsonResult> GetAllCar()
        {
            var query = JsonConvert.DeserializeObject<List<Car>>(await service.GetCar());
            return Json(query);
        }

        // GET api/GetCar/1 (example)
        [Route("api/GetCar/{id}")]
        [HttpGet]
        public async Task<JsonResult> GetCarById(int id)
        {
            var query = JsonConvert.DeserializeObject<List<Car>>(await service.GetCar()).Where(x => x.Id == id);
            return Json(query);
        }

        // POST api/PostCar/Bus&100 (example)
        [Route("api/PostCar/{category}&{cash}")]
        [HttpPost]
        public async Task<JsonResult> PostNewCar(string category, int cash)
        {
            try
            {
                await service.PostCar(category, cash);
                return Json(Ok());
            }
            catch(Exception)
            {
                return Json(BadRequest());
            }
        }

        // DELETE api/RemoveCar/5
        [HttpDelete("api/RemoveCar/{id}")]
        public async Task<JsonResult> DeleteCar(int id)
        {
            try
            {
                await service.DeleteCar(id);
                return Json(Ok());
            }
            catch
            {
                return Json(BadRequest());
            }
        }
        // PUT api/Posts/5
        /*[HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }*/
    }
}
