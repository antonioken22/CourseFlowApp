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
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new OleDbCommand("INSERT INTO Subjects (SubjectCode, SubjectName, CourseID, AcademicYearID, YearLevelID, SemesterID) VALUES (@subjectCode, @subjectName, @courseID, @academicYearID, @yearLevelID, @semesterID)", connection))
                {
                    command.Parameters.AddWithValue("@subjectCode", subjectModel.SubjectCode);
                    command.Parameters.AddWithValue("@subjectName", subjectModel.SubjectName);
                    command.Parameters.AddWithValue("@courseID", subjectModel.CourseID);
                    command.Parameters.AddWithValue("@academicYearID", subjectModel.AcademicYearID);
                    command.Parameters.AddWithValue("@yearLevelID", subjectModel.YearLevelID);
                    command.Parameters.AddWithValue("@semesterID", subjectModel.SemesterID);
                    command.ExecuteNonQuery();
                }
                using (var command = new OleDbCommand("SELECT @@IDENTITY", connection))
                {
                    subjectModel.Id = Convert.ToInt32(command.ExecuteScalar());
                }   
            }
        }

        public void Edit(SubjectModel subjectModel)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new OleDbCommand("UPDATE Subjects SET SubjectCode = @subjectCode, SubjectName = @subjectName, CourseID = @courseID, AcademicYearID = @academicYearID, YearLevelID = @yearLevelID, SemesterID = @semesterID WHERE SubjectID = @subjectID", connection))
                {
                    command.Parameters.AddWithValue("@subjectCode", subjectModel.SubjectCode);
                    command.Parameters.AddWithValue("@subjectName", subjectModel.SubjectName);
                    command.Parameters.AddWithValue("@courseID", subjectModel.CourseID);
                    command.Parameters.AddWithValue("@academicYearID", subjectModel.AcademicYearID);
                    command.Parameters.AddWithValue("@yearLevelID", subjectModel.YearLevelID);
                    command.Parameters.AddWithValue("@semesterID", subjectModel.SemesterID);
                    command.Parameters.AddWithValue("@subjectID", subjectModel.Id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Remove(int id)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new OleDbCommand("DELETE FROM Subjects WHERE SubjectID = @subjectModel", connection))
                {
                    command.Parameters.AddWithValue("@subjectModel", id);
                    command.ExecuteNonQuery();
                }
            }
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
                                CourseID = int.Parse(reader["CourseID"].ToString()),
                                AcademicYearID = int.Parse(reader["AcademicYearID"].ToString()),
                                YearLevelID = int.Parse(reader["YearLevelID"].ToString()),
                                SemesterID = int.Parse(reader["SemesterID"].ToString())
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
