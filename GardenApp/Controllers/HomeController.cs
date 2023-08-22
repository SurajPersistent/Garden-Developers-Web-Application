
using GardenApp.Helper;
using GardenApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http;

namespace GardenApp.Controllers
{
    public class HomeController : Controller
    {
        HttpClient client = new HttpClient();
     
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }

        public IActionResult Services()
        {
            List<Plan> list = new List<Plan>();
            client.BaseAddress = new Uri("https://localhost:7223/api/Plans");
            var responce = client.GetAsync("Plans");
            responce.Wait();

            var test = responce.Result;
            if (test.IsSuccessStatusCode)
            {
                var display = test.Content.ReadAsAsync<List<Plan>>();
                display.Wait();
                list = display.Result;
            }
            return View(list);
        }

        public IActionResult OurWork()
        {
            List<Project> list = new List<Project>();
            var result = client.GetAsync("https://localhost:7223/api/Projects").Result;

            if (result.IsSuccessStatusCode)
            {
                list = result.Content.ReadAsAsync<List<Project>>().Result;
            }
            return View(list);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult ContactUs()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ContactUs(Customer model)
        {
            model.Status = "Active";
            var postTask = client.PostAsJsonAsync<Customer>("https://localhost:7223/api/Customers", model);
            postTask.Wait();


            var result = postTask.Result;
            ViewBag.Message = "Error";
            if (result.IsSuccessStatusCode)
            {
                ViewBag.Message = "Thank You for Connecting with us.";
            }
            return View();
        }
    }
}