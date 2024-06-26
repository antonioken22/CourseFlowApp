﻿using CourseFlow.Models;
using System.Collections.Generic;

namespace CourseFlow.Repositories
{
    public interface ISubjectRepository
    {
        void Add(SubjectModel subjectModel);
        void Edit(SubjectModel subjectModel);
        void Remove(int id);
        SubjectModel GetById(int id);
        IEnumerable<SubjectModel> GetAll();
        public List<SubjectModel> GetSubjectsByCourseAndAcademicYear(CourseModel course, AcademicYearModel academicYear);
    }
}
