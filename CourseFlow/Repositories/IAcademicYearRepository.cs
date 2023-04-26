using CourseFlow.Models;
using System.Collections.Generic;

namespace CourseFlow.Repositories
{
    public interface IAcademicYearRepository
    {
        void Add(AcademicYearModel academicYearModel);
        void Edit(AcademicYearModel academicYearModel);
        void Remove(int id);
        AcademicYearModel GetById(int id);
        IEnumerable<AcademicYearModel> GetAll();
    }
}
