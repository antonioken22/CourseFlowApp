using CourseFlow.Models;
using System;
using System.Collections.Generic;
using System.Data.OleDb;

namespace CourseFlow.Repositories
{
    public class SubjectRepository : RepositoryBase, ISubjectRepository
    {
        public void Add(SubjectModel subjectModel)
        {
            throw new NotImplementedException();
        }

        public void Edit(SubjectModel subjectModel)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SubjectModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public SubjectModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SubjectModel> GetByCourseAndAcademicYear(int courseID, int academicYearID)
        {
            var subjects = new List<SubjectModel>();
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new OleDbCommand("SELECT * FROM Subjects WHERE CourseID = @courseID AND AcademicYearID = @academicYearID", connection))
                {
                    command.Parameters.AddWithValue("@courseID", courseID);
                    command.Parameters.AddWithValue("@academicYearID", academicYearID);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            subjects.Add(new SubjectModel
                            {
                                Id = Convert.ToInt32(reader["SubjectID"]),
                                SubjectCode = reader["SubjectCode"].ToString(),
                                SubjectName = reader["SubjectName"].ToString(),
                                CourseID = Convert.ToInt32(reader["CourseID"]),
                                AcademicYearID = Convert.ToInt32(reader["AcademicYearID"]),
                                YearLevelID = Convert.ToInt32(reader["YearLevelID"]),
                                SemesterID = Convert.ToInt32(reader["SemesterID"]),
                            });
                        }
                    }
                }
            }

            return subjects;
        }
    }
}
