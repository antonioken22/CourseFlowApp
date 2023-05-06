using CourseFlow.Models;
using System;
using System.Collections.Generic;
using System.Data.OleDb;

namespace CourseFlow.Repositories
{
    public class SemesterRepository : RepositoryBase, ISemesterRepository
    {
        public void Add(SemesterModel semesterModel)
        {
            throw new NotImplementedException();
        }

        public void Edit(SemesterModel semesterModel)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public SemesterModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SemesterModel> GetAll()
        {
            var semesters = new List<SemesterModel>();
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new OleDbCommand("SELECT * FROM Semesters", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            semesters.Add(new SemesterModel
                            {
                                Id = Convert.ToInt32(reader["SemesterID"]),
                                Semester = reader["Semester"].ToString(),
                            });
                        }
                    }
                }
            }

            return semesters;
        }

        public IEnumerable<SemesterModel> GetByYearLevelId(int id)
        {
            var semesters = new List<SemesterModel>();
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new OleDbCommand("SELECT * FROM Semesters WHERE ", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            semesters.Add(new SemesterModel
                            {
                                Id = Convert.ToInt32(reader["SemesterID"]),
                                Semester = reader["Semester"].ToString(),
                            });
                        }
                    }
                }
            }

            return semesters;
        }
    }
}
