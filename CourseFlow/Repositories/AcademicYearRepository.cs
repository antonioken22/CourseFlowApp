using CourseFlow.Models;
using System;
using System.Collections.Generic;
using System.Data.OleDb;

namespace CourseFlow.Repositories
{
    public class AcademicYearRepository : RepositoryBase, IAcademicYearRepository
    {
        public void Add(AcademicYearModel academicYearModel)
        {
            // Implement the Add method
        }

        public void Edit(AcademicYearModel academicYearModel)
        {
            // Implement the Edit method
        }

        public void Remove(int id)
        {
            // Implement the Remove method
        }

        public AcademicYearModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AcademicYearModel> GetAll()
        {
            var academicYears = new List<AcademicYearModel>();
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new OleDbCommand("SELECT * FROM AcademicYears", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            academicYears.Add(new AcademicYearModel
                            {
                                Id = Convert.ToInt32(reader["AcademicYearID"]),
                                AcademicYear = reader["AcademicYear"].ToString(),
                            });
                        }
                    }
                }
            }

            return academicYears;
        }
    }
}
