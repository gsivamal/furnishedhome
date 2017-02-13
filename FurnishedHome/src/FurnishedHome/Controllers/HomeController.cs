using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FurnishedHome.Services;

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
            return Json(_propertyService.GetAllProperties());
        }
        [HttpPost]
        public JsonResult GetProperty(long id)
        {
            return Json(_propertyService.GetPropertyById(id));
        }

        public IActionResult Property(long id)
        {
            var model = _propertyService.GetPropertyById(id);
            return View(model.Id);
        }
    }
}
