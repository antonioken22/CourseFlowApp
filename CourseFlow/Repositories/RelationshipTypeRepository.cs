using CourseFlow.Models;
using System;
using System.Collections.Generic;
using System.Data.OleDb;

namespace CourseFlow.Repositories
{
    public class RelationshipTypeRepository : RepositoryBase, IRelationshipTypeRepository
    {
        public void Add(RelationshipTypeModel relationshipTypeModel)
        {
            // Implement the Add method
        }

        public void Edit(RelationshipTypeModel relationshipTypeModel)
        {
            // Implement the Edit method
        }

        public void Remove(int id)
        {
            // Implement the Remove method
        }

        public RelationshipTypeModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RelationshipTypeModel> GetAll()
        {
            var relationshipTypes = new List<RelationshipTypeModel>();
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new OleDbCommand("SELECT * FROM RelationshipTypes", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            relationshipTypes.Add(new RelationshipTypeModel
                            {
                                Id = Convert.ToInt32(reader["RelationshipTypeID"]),
                                RelationshipType = reader["RelationshipType"].ToString(),
                            });
                        }
                    }
                }
            }

            return relationshipTypes;
        }
    }
}
