using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FurnishedHome.Services;

namespace FurnishedHome.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        private readonly IPropertyService _propertyService;

        public HomeController(IPropertyService propertyService)
        {
            _propertyService = propertyService;
        }

        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("[action]")]
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        [Route("[action]")]
        public IActionResult HowItWorks()
        {
            return View();
        }

        [Route("[action]")]
        public IActionResult Testimonials()
        {
            return View();
        }

        [Route("[action]")]
        public IActionResult Dallas()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetAllProperties()
        {
            return Json(_propertyService.GetAllProperties());
        }
    }
}
