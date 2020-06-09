using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using MyCourse.Models.Services.Applications;
using MyCourse.Models.ViewModels;
using System.Threading.Tasks;

namespace MyCourse.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ICourseService courseService;
        public CoursesController(ICourseService courseService)
        {
            this.courseService=courseService;
        }

        public IActionResult Index()
        {
            ViewBag.Title = "Catalogo dei corsi";   
            List<CourseViewModel> courses = courseService.GetCourses();
            return View(courses);
        }


        public IActionResult Detail(int id)
        {
           
           CourseDetailViewModel viewModel = courseService.GetCourse(id);
            ViewBag.Title = viewModel.Title;
            return View(viewModel);
        }
    }
}













