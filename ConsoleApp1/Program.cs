using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;


namespace ConsoleApp1
{
    class Program
    {
        static async Task Main()
        {
            while(true)
            {
                await Menu.Start();
            }
        }
    }
}
