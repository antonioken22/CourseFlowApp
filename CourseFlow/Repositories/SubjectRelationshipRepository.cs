using CourseFlow.Models;
using System;
using System.Collections.Generic;
using System.Data.OleDb;

namespace CourseFlow.Repositories
{
    public class SubjectRelationshipRepository : RepositoryBase, ISubjectRelationshipRepository
    {
        public bool Exists(SubjectRelationshipModel subjectRelationshipModel)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new OleDbCommand("SELECT COUNT(*) FROM SubjectRelationships WHERE SubjectID = @subjectID AND RelatedSubjectID = @relatedSubjectID AND RelationshipTypeID = @relationshipTypeID", connection))
                {
                    command.Parameters.AddWithValue("@subjectID", subjectRelationshipModel.SubjectID);
                    command.Parameters.AddWithValue("@relatedSubjectID", subjectRelationshipModel.RelatedSubjectID);
                    command.Parameters.AddWithValue("@relationshipTypeID", subjectRelationshipModel.RelationshipTypeID);
                    var count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        public void AddOrEdit(SubjectRelationshipModel subjectRelationshipModel)
        {
            if (!Exists(subjectRelationshipModel))
            {
                using (var connection = GetConnection())
                {
                    connection.Open();
                    using (var command = new OleDbCommand("INSERT INTO SubjectRelationships (SubjectID, RelatedSubjectID, RelationshipTypeID) VALUES (@subjectID, @relatedSubjectID, @relationshipTypeID)", connection))
                    {
                        command.Parameters.AddWithValue("@subjectID", subjectRelationshipModel.SubjectID);
                        command.Parameters.AddWithValue("@relatedSubjectID", subjectRelationshipModel.RelatedSubjectID);
                        command.Parameters.AddWithValue("@relationshipTypeID", subjectRelationshipModel.RelationshipTypeID);
                        command.ExecuteNonQuery();
                    }
                }
            }
            else
            {
                using (var connection = GetConnection())
                {
                    connection.Open();
                    using (var command = new OleDbCommand("UPDATE SubjectRelationships SET SubjectID = @subjectID, RelatedSubjectID = @relatedSubjectID, RelationshipTypeID = @relationshipTypeID WHERE SubjectID = @subjectID AND RelatedSubjectID = @relatedSubjectID AND RelationshipTypeID = @relationshipTypeID", connection))
                    {
                        command.Parameters.AddWithValue("@subjectID", subjectRelationshipModel.SubjectID);
                        command.Parameters.AddWithValue("@relatedSubjectID", subjectRelationshipModel.RelatedSubjectID);
                        command.Parameters.AddWithValue("@relationshipTypeID", subjectRelationshipModel.RelationshipTypeID);
                        command.ExecuteNonQuery();
                    }
                }
            }
        }

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
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new OleDbCommand("DELETE FROM SubjectRelationships WHERE Id = @id", connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void RemoveBySubjectId(int subjectId)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new OleDbCommand("DELETE FROM SubjectRelationships WHERE SubjectID = @subjectID", connection))
                {
                    command.Parameters.AddWithValue("@subjectID", subjectId);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void RemoveBySubjectIdAndRelatedSubjectCode(int subjectId, object parameter)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new OleDbCommand("SELECT * FROM Subjects WHERE SubjectCode = @subjectCode", connection))
                {
                    command.Parameters.AddWithValue("@subjectCode", parameter);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var relatedSubjectId = Convert.ToInt32(reader["SubjectID"]);
                            using (var command2 = new OleDbCommand("DELETE FROM SubjectRelationships WHERE SubjectID = @subjectID AND RelatedSubjectID = @relatedSubjectID", connection))
                            {
                                command2.Parameters.AddWithValue("@subjectID", subjectId);
                                command2.Parameters.AddWithValue("@relatedSubjectID", relatedSubjectId);
                                command2.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }
        }

        public SubjectRelationshipModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SubjectRelationshipModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<SubjectRelationshipModel> GetSubjectRelationshipsBySubject(int id)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new OleDbCommand("SELECT * FROM SubjectRelationships WHERE SubjectID = @subjectID", connection))
                {
                    command.Parameters.AddWithValue("@subjectID", id);
                    using (var reader = command.ExecuteReader())
                    {
                        var subjectRelationships = new List<SubjectRelationshipModel>();
                        while (reader.Read())
                        {
                            var subjectRelationship = new SubjectRelationshipModel
                            {
                                Id = Convert.ToInt32(reader["RelationshipID"]),
                                SubjectID = Convert.ToInt32(reader["SubjectID"]),
                                RelatedSubjectID = Convert.ToInt32(reader["RelatedSubjectID"]),
                                RelationshipTypeID = Convert.ToInt32(reader["RelationshipTypeID"])
                            };
                            subjectRelationships.Add(subjectRelationship);
                        }
                        return subjectRelationships;
                    }
                }   
            }
        }
    }
}
