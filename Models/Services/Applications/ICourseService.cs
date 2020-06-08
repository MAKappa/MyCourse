using System.Collections.Generic;
using System.Threading.Tasks;
using MyCourse.Models.ViewModels;

namespace MyCourse.Models.Services.Applications
{
    public interface ICourseService
    {
        List<CourseViewModel> GetCourses();
         CourseDetailViewModel GetCourse(int id);
         
    }
}