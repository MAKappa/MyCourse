using System.Collections.Generic;


namespace MyCourse.Models.ViewModels
{
    public class CourseDetailViewModel : CourseViewModel
    {
        
        public string Description { get; set; }
        public List<LessonViewModel> Lessons { get; set; }
    }
}