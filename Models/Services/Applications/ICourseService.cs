using System.Collections.Generic;
using System.Threading.Tasks;
using MyCourse.Models.ViewModels;

namespace MyCourse.Models.Services.Applications
{
    public interface ICourseService
    {
         Task<List<CourseViewModel>> GetCoursesAsync(string search, int page);
         Task<CourseDetailViewModel> GetCourseAsync(int id);
         
    }
}