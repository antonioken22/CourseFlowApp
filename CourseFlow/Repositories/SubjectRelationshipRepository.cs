using CourseFlow.Models;
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
            throw new NotImplementedException();
        }

        public List<SubjectRelationshipModel> GetRelatedSubjects(SubjectModel subject)
        {
            var relatedSubjects = new List<SubjectRelationshipModel>();

            using (var connection = GetConnection())
            {
                throw new NotImplementedException();
            }
        }
    }
}
