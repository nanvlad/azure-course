using System;
using System.Threading.Tasks;
using AzureCourse.Models;
using AzureCourse.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AzureCourse.Controllers
{
    [Route("[controller]/[action]")]
    public class ImagesController : Controller
    {
        private readonly ImageStore _store;

        public ImagesController(ImageStore store)
        {
            _store = store;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(IFormFile image)
        {
            if(image != null)
            {
                using (var stream = image.OpenReadStream())
                {
                    string imageId = await _store.SaveAsync(stream);
                    return RedirectToAction(nameof(Show), new { imageId });
                }
            }

            return View();
        }

        [HttpGet("{imageId}")]
        public ActionResult Show(string imageId)
        {
            var model = new ShowModel { Uri = new Uri(_store.UriFor(imageId)) };
            return View(model);
        }
    }
}