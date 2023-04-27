﻿using CourseFlow.Models;
using System;
using System.Collections.Generic;
using System.Data.OleDb;

namespace CourseFlow.Repositories
{
    public class SubjectRelationshipRepository : RepositoryBase, ISubjectRelationshipRepository
    {
        public void Add(SubjectRelationshipModel subjectRelationshipModel)
        {
            throw new NotImplementedException();
        }

        public void Edit(SubjectRelationshipModel subjectRelationshipModel)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SubjectRelationshipModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public SubjectRelationshipModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SubjectRelationshipModel> GetBySubjectID(int subjectID)
        {
            var subjectRelationships = new List<SubjectRelationshipModel>();
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new OleDbCommand("SELECT * FROM SubjectRelationships WHERE SubjectID = @subjectID", connection))
                {
                    command.Parameters.AddWithValue("@subjectID", subjectID);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            subjectRelationships.Add(new SubjectRelationshipModel
                            {
                                RelationshipID = Convert.ToInt32(reader["RelationshipID"]),
                                SubjectID = Convert.ToInt32(reader["SubjectID"]),
                                RelatedSubjectID = Convert.ToInt32(reader["RelatedSubjectID"]),
                                RelationshipType = reader["RelationshipType"].ToString(),
                            });
                        }
                    }
                }
            }

            return subjectRelationships;
        }
    }
}
