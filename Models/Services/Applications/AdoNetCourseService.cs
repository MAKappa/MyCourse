using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using MyCourse.Models.Options;
using MyCourse.Models.Services.Infrastructure;
using MyCourse.Models.ViewModels;
using Microsoft.Extensions.Options;




namespace MyCourse.Models.Services.Applications
{
    public class AdoNetCourseService : ICourseService
    {
        private readonly IDatabaseAccesser db;
        private readonly IOptionsMonitor<CoursesOptions> coursesOptions;

        public AdoNetCourseService(IDatabaseAccesser db, IOptionsMonitor<CoursesOptions> coursesOptions)
        {
            this.db=db;
            this.coursesOptions=coursesOptions;
        }

        

        public async Task<CourseDetailViewModel> GetCourseAsync(int id)
        {
            
            var courseList= new List<CourseViewModel>();
            FormattableString query = $@"SELECT Id, Title, Description, ImagePath, Author, Rating, FullPrice_Amount, FullPrice_Currency, CurrentPrice_Amount, CurrentPrice_Currency FROM Courses WHERE Id={id}
            ; SELECT Id, Title, Description, Duration FROM Lessons WHERE CourseId={id}";
            DataSet dataSet = await db.QueryAsync(query);

            //Course
            var courseTable = dataSet.Tables[0];
            if (courseTable.Rows.Count != 1) {
                throw new InvalidOperationException($"Did not return exactly 1 row for Course {id}");
            }
            var courseRow = courseTable.Rows[0];
            var courseDetailViewModel = CourseDetailViewModel.FromDataRow(courseRow);

            //Course lessons
            var lessonDataTable = dataSet.Tables[1];

            foreach(DataRow lessonRow in lessonDataTable.Rows) 
            {
                LessonViewModel lessonViewModel = LessonViewModel.FromDataRow(lessonRow);
                courseDetailViewModel.Lessons.Add(lessonViewModel);
            }
            return courseDetailViewModel; 
        }
        
        


        public async Task<List<CourseViewModel>> GetCoursesAsync()
        {
            var courseList= new List<CourseViewModel>();
            FormattableString query = $"SELECT Id, Title, ImagePath, Author, Rating, FullPrice_Amount, FullPrice_Currency, CurrentPrice_Amount, CurrentPrice_Currency FROM Courses";
            DataSet dataSet = await db.QueryAsync(query);
            var dataTable= dataSet.Tables[0];
            foreach (DataRow courseRow in dataTable.Rows)
            {
                CourseViewModel course= CourseViewModel.FromDataRow(courseRow);
                courseList.Add(course);
            }
            return courseList;
        }
    }

    
}