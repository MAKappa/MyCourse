using System.Net.Mime;
using System.Collections.Generic;
using MyCourse.Models.Services.Applications;
using MyCourse.Models.Services.Infrastructure;
using MyCourse.Models.ViewModels;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MyCourse.Models.Services.Infrastructure
{
    public class EFCoreCourseService : ICourseService
    {
        private readonly MyCourseDbContext dbContext;
        public EFCoreCourseService(MyCourseDbContext dbContext)
        {
            this.dbContext = dbContext;

        }

    

        public async Task<CourseDetailViewModel> GetCourseAsync(int id)
        {
             IQueryable<CourseDetailViewModel> queryLinq =  dbContext.Courses
            .AsNoTracking()
            .Include(course  => course.Lessons)
            .Where(course => course.Id == id)
            .Select(course =>  CourseDetailViewModel.FromEntity(course));
            
            CourseDetailViewModel viewModel= await queryLinq.SingleAsync();
            return viewModel;
        }

        

        public async Task<List<CourseViewModel>> GetCoursesAsync()
        {
            IQueryable<CourseViewModel> queryLinq = dbContext.Courses
            .AsNoTracking()
            .Select(course => CourseViewModel.FromEntity(course));
            List<CourseViewModel> courses = await queryLinq.ToListAsync();
            return courses;
        }
    }
}