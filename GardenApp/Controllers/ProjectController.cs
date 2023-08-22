using GardenApp.Helper;
using GardenApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace GardenApp.Controllers
{
    public class ProjectController : Controller
    {
        HttpClient client = new HttpClient();
        public IActionResult AddProject()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddProject([FromForm] Image image)
        {
            Project project = new Project();
            //var ImageUrl = await FileHelper.UploadImage(image.ImageFile);
            try
            {
                var path = Path.Combine(@"C:\Users\suraj_sutar\OneDrive - Persistent Systems Limited\Desktop\Project\GardenApp\GardenApp\wwwroot\Content\Project", image.ImageFile.FileName);
                using(Stream stream = new FileStream(path,FileMode.Create))
                {
                    image.ImageFile.CopyTo(stream);
                }
            }
            catch (Exception ex) { }
            project.ImageUrl = Path.Combine(@"/Content/Project/", image.ImageFile.FileName);
            //   var content = new FormUrlEncodedContent((IEnumerable<KeyValuePair<string, string>>)plan);
            project.Description = image.PlanName;
            var postTask = client.PostAsJsonAsync<Project>("https://localhost:7223/api/Projects", project);
            postTask.Wait();

            var result = postTask.Result;
            ViewBag.Message = "Error";
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index","Admin");
            }
            return View();
        }
    }
}
