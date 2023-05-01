using CourseFlow.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CourseFlow.Repositories
{
    public interface ISubjectRepository
    {
        void Add(SubjectModel subjectModel);
        void Edit(SubjectModel subjectModel);
        void Remove(int id);
        SubjectModel GetById(int id);
        IEnumerable<SubjectModel> GetAll();
        IEnumerable<SubjectModel> GetByCourseAndAcademicYear(int courseID, int academicYearID);
        public List<SubjectModel> GetSubjectsByYearLevelSemesterAndCourse(YearLevelModel yearLevel, SemesterModel semester, CourseModel course, AcademicYearModel academicYear);
        public List<SubjectModel> GetSubjectsByCourseAndAcademicYear(CourseModel course, AcademicYearModel academicYear);
    }
}
