using Microsoft.AspNetCore.Mvc;
using FurnishedHome.Services;
using FurnishedHome.Entities;
using MongoDB.Bson;
using Microsoft.AspNetCore.Authorization;

namespace FurnishedHome.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPropertyService _propertyService;

        public HomeController(IPropertyService propertyService)
        {
            _propertyService = propertyService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult HowItWorks()
        {
            return View();
        }

        public IActionResult Testimonials()
        {
            return View();
        }

        public IActionResult Dallas()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetAllProperties()
        {
            var properties = _propertyService.GetAllProperties();
            return Json(properties);
        }
        [HttpPost]
        public JsonResult GetProperty(ObjectId id)
        {
            return Json(_propertyService.GetPropertyById(id));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public void AddProperty(Property property)
        {
            _propertyService.AddProperty(property);
        }

        public IActionResult Property(ObjectId id)
        {
            var model = _propertyService.GetPropertyById(id);
            return View(model.Id);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Admin()
        {
            return View();
        }
    }
}
