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
        public SubjectModel GetById(int id)
        {
            SubjectModel subject = null;

            using (var connection = GetConnection())
            {
                connection.Open();

                using (var command = new OleDbCommand("SELECT * FROM Subjects WHERE SubjectID = @subjectID", connection))
                {
                    command.Parameters.AddWithValue("@subjectID", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            subject = new SubjectModel
                            {
                                Id = Convert.ToInt32(reader["SubjectID"]),
                                SubjectCode = reader["SubjectCode"].ToString(),
                                SubjectName = reader["SubjectName"].ToString(),
                                // Add other properties as needed
                            };
                        }
                    }
                }
            }

            return subject;

        }

        public IEnumerable<SubjectModel> GetAll()
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

        public List<SubjectModel> GetSubjectsByYearLevelSemesterAndCourse(YearLevelModel yearLevel, SemesterModel semester, CourseModel course, AcademicYearModel academicYear)
        {
            var subjects = new List<SubjectModel>();
            using (var connection = GetConnection())
            {
                connection.Open();
                string baseQuery = "SELECT * FROM Subjects WHERE CourseID = @courseID AND AcademicYearID = @academicYearID AND YearLevelID = @yearLevelID";
                string semesterFilter = semester != null ? " AND SemesterID = @semesterID" : "";
                string query = baseQuery + semesterFilter;

                using (var command = new OleDbCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@courseID", course.Id);
                    command.Parameters.AddWithValue("@academicYearID", academicYear.Id);
                    command.Parameters.AddWithValue("@yearLevelID", yearLevel.Id);
                    if (semester != null)
                    {
                        command.Parameters.AddWithValue("@semesterID", semester.Id);
                    }

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


        public List<SubjectModel> GetSubjectsByCourseAndAcademicYear(CourseModel course, AcademicYearModel academicYear)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                var query = "SELECT * FROM Subjects WHERE CourseID = @CourseID AND AcademicYearID = @AcademicYearID";
                var command = new OleDbCommand(query, connection);
                command.Parameters.AddWithValue("@CourseID", course.Id);
                command.Parameters.AddWithValue("@AcademicYearID", academicYear.Id);

                var reader = command.ExecuteReader();
                var subjects = new List<SubjectModel>();

                while (reader.Read())
                {
                    var subject = new SubjectModel
                    {
                        Id = (int)reader["SubjectID"],
                        SubjectCode = reader["SubjectCode"].ToString(),
                        SubjectName = reader["SubjectName"].ToString(),
                        CourseID = (int)reader["CourseID"],
                        AcademicYearID = (int)reader["AcademicYearID"],
                        YearLevelID = (int)reader["YearLevelID"],
                        SemesterID = (int)reader["SemesterID"]
                    };
                    subjects.Add(subject);
                }

                return subjects;
            }
        }
    }
}
