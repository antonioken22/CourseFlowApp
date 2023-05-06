using CourseFlow.Models;
using System.Collections.Generic;

namespace CourseFlow.Repositories
{
    public interface ISemesterRepository
    {
        void Add(SemesterModel semesterModel);
        void Edit(SemesterModel semesterModel);
        void Remove(int id);
        SemesterModel GetById(int id);
        IEnumerable<SemesterModel> GetAll();
        IEnumerable<SemesterModel> GetByYearLevelId(int id);
    }
}
