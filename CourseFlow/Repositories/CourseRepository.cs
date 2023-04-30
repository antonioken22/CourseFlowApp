using CourseFlow.Models;
using System;
using System.Collections.Generic;
using System.Data.OleDb;

namespace CourseFlow.Repositories
{
    public class CourseRepository : RepositoryBase, ICourseRepository
    {
        public void Add(CourseModel courseModel)
        {
            // Implement the Add method
        }

        public void Edit(CourseModel courseModel)
        {
            // Implement the Edit method
        }

        public void Remove(int id)
        {
            // Implement the Remove method
        }

        public CourseModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CourseModel> GetAll()
        {
            var courses = new List<CourseModel>();
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new OleDbCommand("SELECT * FROM Courses", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            courses.Add(new CourseModel
                            {
                                Id = Convert.ToInt32(reader["CourseID"]),
                                CourseName = reader["CourseName"].ToString(),
                            });
                        }
                    }
                }
            }

            return courses;
        }
    }

}
