using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace LiftMaps.Controllers
{
    public class HomeController : Controller
    {
        static string _address = "https://liftie.info/api/resort/whistler-blackcomb";
        static string _address2 = "https://liftie.info/api/resort/stevens";
        
        // GET: /Home/
        [HttpGet]
        [Route("/whistler")]
        public async Task<IActionResult> Index()
        {
            var res = await GetExternalResponse(_address);
            JObject json = JObject.Parse(res);
            // var status =  Get();
            System.Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!");
            System.Console.WriteLine("INDEX RES: "+json);
            System.Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!");
            ViewBag.status = json["lifts"]["status"];
            ViewBag.stats = json["lifts"]["stats"];
            ViewBag.weather = json["weather"]["text"];
            ViewBag.conditions = json["weather"]["conditions"];
            return View("Index");
        }

        [HttpGet("/stevens")]
        public async Task<IActionResult> Stevens()
        {
            var res = await GetExternalResponse(_address2);
            JObject json = JObject.Parse(res);
            System.Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!");
            System.Console.WriteLine("STEVENS RES: "+json);
            System.Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!");
            ViewBag.status = json["lifts"]["status"];
            ViewBag.stats = json["lifts"]["stats"];
            ViewBag.weather = json["weather"]["text"];
            ViewBag.conditions = json["weather"]["conditions"];
            return View();
        }
        private async Task<string> GetExternalResponse(string address)
        {
            var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(address);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            return result;
        }
    }
}
