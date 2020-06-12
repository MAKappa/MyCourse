using System.Net.Mime;
using System.Collections.Generic;
using MyCourse.Models.Services.Applications;
using MyCourse.Models.Services.Infrastructure;
using MyCourse.Models.ViewModels;
using System.Linq;

namespace MyCourse.Models.Services.Infrastructure
{
    public class EFCoreCourseService : ICourseService
    {
        private readonly MyCourseDbContext dbContext;
        public EFCoreCourseService(MyCourseDbContext dbContext)
        {
            this.dbContext = dbContext;

        }

        public  CourseDetailViewModel GetCourse(int id)
        {
            CourseDetailViewModel viewModel= dbContext.Courses
            .Where(course => course.Id == id)
            .Select(course => new CourseDetailViewModel
            {
                Id=course.Id,
                Title=course.Title,
                ImagePath=course.ImagePath,
                Author=course.Author,
                Rating=course.Rating,
                CurrentPrice=course.CurrentPrice,
                FullPrice=course.FullPrice
            }).Single();
            
            return viewModel;
        }

        public List<CourseViewModel> GetCourses()
        {
            List<CourseViewModel> courses = dbContext.Courses.Select(course =>
            new CourseViewModel{
                Id=course.Id,
                Title=course.Title,
                ImagePath=course.ImagePath,
                Author=course.Author,
                Rating=course.Rating,
                CurrentPrice=course.CurrentPrice,
                FullPrice=course.FullPrice


            }).ToList();
            return courses;
        }
    }
}