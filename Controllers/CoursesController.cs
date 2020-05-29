using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using MyCourse.Models.Services.Applications;
using MyCourse.Models.ViewModels;

namespace MyCourse.Controllers
{
    public class CoursesController : Controller
    {
        public IActionResult Index()
        {
            CourseService courseService = new CourseService();
            List<CourseViewModel> courses= courseService.GetServices();

            return View(courses);
        }


        public IActionResult Detail(int id)
        {
            return View();
        }
    }
}