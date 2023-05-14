using CourseFlow.Models;
using System;
using System.Collections.Generic;
using System.Data.OleDb;

namespace CourseFlow.Repositories
{
    public class YearLevelRepository : RepositoryBase, IYearLevelRepository
    {
        public void Add(YearLevelModel yearLevelModel)
        {
            throw new NotImplementedException();
        }

        public void Edit(YearLevelModel yearLevelModel)
        {
            throw new NotImplementedException();
        }
        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public YearLevelModel GetById(int id)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                var query = "SELECT * FROM YearLevels WHERE YearLevelID = @YearLevelID";
                var command = new OleDbCommand(query, connection);
                command.Parameters.AddWithValue("@YearLevelID", id);

                var reader = command.ExecuteReader();

                if (reader.Read())
                {
                    var yearLevel = new YearLevelModel
                    {
                        Id = (int)reader["YearLevelID"],
                        YearLevel = reader["YearLevel"].ToString()
                    };
                    return yearLevel;
                }
                else
                {
                    return null;
                }
            }
        }

        public IEnumerable<YearLevelModel> GetAll()
        {
            var yearLevels = new List<YearLevelModel>();
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new OleDbCommand("SELECT * FROM YearLevels", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            yearLevels.Add(new YearLevelModel
                            {
                                Id = Convert.ToInt32(reader["YearLevelID"]),
                                YearLevel = reader["YearLevel"].ToString(),
                            });
                        }
                    }
                }
            }

            return yearLevels;
        }
    }
}
