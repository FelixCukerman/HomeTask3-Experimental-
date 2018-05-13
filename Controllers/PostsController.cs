using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HomeTask3_Experimental_.Services;

namespace HomeTask3_Experimental_.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class PostsController : Controller
    {
        private LoadUsersService service {get; set;}
        public PostsController(LoadUsersService service)
        {
            this.service = service;
        }

        // GET api/Posts
        [HttpGet]
        public async Task<List<Posts>> Get()
        {
            return await service.GetPosts();
        }

        // GET api/Posts/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<IEnumerable<Posts>> Get(int id)
        {
            return (await service.GetPosts()).Where(x => x.UserId == id);
        }

        // POST api/Posts
        /*[HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/Posts/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/Posts/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }*/
    }
}
