using System;
using System.Net.Mime;
using System.Collections.Generic;
using MyCourse.Models.Services.Applications;
using MyCourse.Models.Services.Infrastructure;
using MyCourse.Models.ViewModels;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MyCourse.Models.Options;

namespace MyCourse.Models.Services.Infrastructure
{
    public class EFCoreCourseService : ICourseService
    {
        private readonly MyCourseDbContext dbContext;
        private readonly IOptionsMonitor<CoursesOptions> coursesOptions;
        public EFCoreCourseService(MyCourseDbContext dbContext, IOptionsMonitor<CoursesOptions> coursesOptions)
        {
            this.coursesOptions = coursesOptions;
            this.dbContext = dbContext;

        }



        public async Task<CourseDetailViewModel> GetCourseAsync(int id)
        {
            IQueryable<CourseDetailViewModel> queryLinq = dbContext.Courses
           .AsNoTracking()
           .Include(course => course.Lessons)
           .Where(course => course.Id == id)
           .Select(course => CourseDetailViewModel.FromEntity(course));

            CourseDetailViewModel viewModel = await queryLinq.SingleAsync();
            return viewModel;
        }



        public async Task<List<CourseViewModel>> GetCoursesAsync(string search, int page)
        {
            search = search ?? ""; // Questo si utilizza quando un querry ci da risultato null in quel caso invece di restituire risultato null ci da ""
            page = Math.Max(1, page);
            int limit = coursesOptions.CurrentValue.PerPage;
            int offset=(page-1) * limit;
            IQueryable<CourseViewModel> queryLinq = dbContext.Courses
            .Skip(offset)
            .Take(limit)
            .Where(course => course.Title.Contains(search))
            .AsNoTracking()
            .Select(course => CourseViewModel.FromEntity(course));
            List<CourseViewModel> courses = await queryLinq.ToListAsync();
            return courses;
        }
    }
}