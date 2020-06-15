using System.Collections.Generic;
using System.Threading.Tasks;
using MyCourse.Models.ViewModels;

namespace MyCourse.Models.Services.Applications
{
    public interface ICourseService
    {
         Task<List<CourseViewModel>> GetCoursesAsync();
         Task<CourseDetailViewModel> GetCourseAsync(int id);
         
    }
}