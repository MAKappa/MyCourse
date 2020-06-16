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

        public async Task<IActionResult> Index(string search, int page, string order, bool ascending)
        {
            ViewBag.Title = "Catalogo dei corsi";   
            List<CourseViewModel> courses = await courseService.GetCoursesAsync(search, page);
            return View(courses);
        }


        public async Task<IActionResult> Detail(int id)
        {
           
           CourseDetailViewModel viewModel = await courseService.GetCourseAsync(id);
            ViewBag.Title = viewModel.Title;
            return View(viewModel);
        }
    }
}













