using GardenApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace GardenApp.Controllers
{
    public class LoginController : Controller
    {
        private readonly HttpClient client = new HttpClient();
        //public IActionResult Index()
        //{
        //    return View();
        //}

        //public IActionResult CustomerLogin(Admin login)
        //{
        //    List<Customer> list = new List<Customer>();
        //    var result = client.GetAsync("https://localhost:7123/api/Customers").Result;
        //    if (result.IsSuccessStatusCode)
        //    {
        //        list = result.Content.ReadAsAsync<List<Customer>>().Result;
        //    }
        //    foreach (var item in list)
        //    {
        //        if(item.Email == login.UserName && item.Password == login.Password)
        //        {
        //            TempData["name"] = item.CustomerName;
        //            TempData["Id"] = item.Customer_Id;
        //            return RedirectToAction("Index", "Customer");
        //        }
        //    }
        //    return View();
        //}
        public IActionResult AdminLogin(Admin login)
        {
            if (login.UserName == null)
                return View();
            List<Admin> list = new List<Admin>();
            var result = client.GetAsync("https://localhost:7223/api/Admins").Result;
            if (result.IsSuccessStatusCode)
            {
                list = result.Content.ReadAsAsync<List<Admin>>().Result;
            }
            foreach (var item in list)
            {
                if (item.UserName == login.UserName && item.Password == login.Password)
                {
                    ViewBag.loggedIn = true;
                    return RedirectToAction("Index", "Admin");
                }
            }
            ViewBag.Message = "Invalid Username or Password";
            return View();
        }

    }
}
