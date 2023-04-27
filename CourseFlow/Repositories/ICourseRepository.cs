using CourseFlow.Models;
using System.Collections.Generic;

namespace CourseFlow.Repositories
{
    public interface ICourseRepository
    {
        void Add(CourseModel courseModel);
        void Edit(CourseModel courseModel);
        void Remove(int id);
        CourseModel GetById(int id);
        IEnumerable<CourseModel> GetAll();
    }
}
