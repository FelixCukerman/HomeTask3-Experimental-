using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;

namespace HomeTask3_Experimental_.Services
{
    public class LoadUsersService
    {
        private HttpClient Client { get; set; }

        public LoadUsersService()
        {
            Client = new HttpClient();
        }

        public async Task<List<Posts>> GetPosts()
        {
            var strData = await Client.GetStringAsync("https://jsonplaceholder.typicode.com/posts");

            return JsonConvert.DeserializeObject<List<Posts>>(strData);
        }
    }

    public class Posts
    {
        public int UserId { get; set; }
        public int Id { get; set; }
        public string Body { get; set; }
        public string Title { get; set; }
    }
}