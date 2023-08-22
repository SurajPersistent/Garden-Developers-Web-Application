
using GardenApp.Helper;
using GardenApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;

namespace GardenApp.Controllers
{
    public class AdminController : Controller
    {
        HttpClient client = new HttpClient();
        public IActionResult Index()
        {
            List<Customer> list = new List<Customer>();
            var result = client.GetAsync("https://localhost:7223/api/Customers").Result;
            if (result.IsSuccessStatusCode)
            {
                list = result.Content.ReadAsAsync<List<Customer>>().Result;
            }
            return View(list);
        }


        public ActionResult DeleteCustomer(int id)
        {
            Customer customer = new Customer();
            var result = client.GetAsync("https://localhost:7223/api/Customers/" + id).Result;
            if (result.IsSuccessStatusCode)
            {
                customer = result.Content.ReadAsAsync<Customer>().Result;
            }
            customer.Status = "Complete";
            var postTask = client.PutAsJsonAsync<Customer>("https://localhost:7223/api/Customers", customer);
            postTask.Wait();


            var re = postTask.Result;
            List<Customer> list = new List<Customer>();
            var result1 = client.GetAsync("https://localhost:7223/api/Customers").Result;
            if (result1.IsSuccessStatusCode)
            {
                list = result1.Content.ReadAsAsync<List<Customer>>().Result;
            }

            return View(list);
        }

        public IActionResult CreatePlan()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreatePlanAsync([FromForm] Image image)
        {
            Plan plan = new Plan();
            // plan.ImageUrl = await FileHelper.UploadImage(image.ImageFile);
            try
            {
                var path = Path.Combine(@"C:\Users\suraj_sutar\OneDrive - Persistent Systems Limited\Desktop\Project\GardenApp\GardenApp\wwwroot\Content\Project", image.ImageFile.FileName);
                using (Stream stream = new FileStream(path, FileMode.Create))
                {
                    image.ImageFile.CopyTo(stream);
                }
            }
            catch (Exception ex) { }
            plan.ImageUrl = Path.Combine(@"/Content/Project/", image.ImageFile.FileName);
            //   var content = new FormUrlEncodedContent((IEnumerable<KeyValuePair<string, string>>)plan);
            plan.PlanName = image.PlanName;
            plan.PlanCategory = image.PlanCategory;
            plan.CostPerSqft = image.CostPerSqft;
            plan.MinQuantity = image.MinQuantity; 
            plan.ExpectedDuration = image.ExpectedDuration;
            var postTask = client.PostAsJsonAsync<Plan>("https://localhost:7223/api/Plans", plan);
            postTask.Wait();

            var result = postTask.Result;
            ViewBag.Message = "Error";
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("PlanDetails");
            }
            return View();
        }

        public IActionResult PlanDetails()
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
        public IActionResult DeletePlan(int id)
        {
            Plan transaction = new Plan();
            var result = client.GetAsync("https://localhost:7223/api/Plans/"+id).Result;
            if (result.IsSuccessStatusCode)
            {
                transaction = result.Content.ReadAsAsync<Plan>().Result;
            }

            var postTask = client.DeleteAsync("https://localhost:7223/api/Plans/"+id);
            ViewBag.Message = "Error";
            var re = postTask.Result;
            if (re.IsSuccessStatusCode)
            {
                ViewBag.Message = "Plan deleted successfully";
            }

            return View(transaction);
        }

        public ActionResult EditPlan(int id,Plan plan)
        {
            Plan planObj = new Plan();
            var result = client.GetAsync("https://localhost:7223/api/Plans/" + id).Result;
            if(result.IsSuccessStatusCode)
            {
                planObj = result.Content.ReadAsAsync<Plan>().Result;
            }
            plan.PlanId = id;
            if (plan.CostPerSqft != 0)
            {
                plan.PlanName = planObj.PlanName;
                plan.PlanCategory = planObj.PlanCategory;
                plan.MinQuantity = planObj.MinQuantity;
                plan.ImageUrl = planObj.ImageUrl;
            }
            var postTask = client.PutAsJsonAsync<Plan>("https://localhost:7223/api/Plans", plan);
            postTask.Wait();
            var re = postTask.Result;
            if (re.IsSuccessStatusCode)
            {
                return RedirectToAction("PlanDetails");
            }

            return View(planObj);
        }

    }
}
