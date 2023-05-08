using CourseFlow.Models;
using System.Collections.Generic;

namespace CourseFlow.Repositories
{
    public interface ISubjectRelationshipRepository
    {
        void Add(SubjectRelationshipModel subjectRelationshipModel);
        void Edit(SubjectRelationshipModel subjectRelationshipModel);
        void Remove(int id);
        SubjectRelationshipModel GetById(int id);
        IEnumerable<SubjectRelationshipModel> GetAll();
        IEnumerable<SubjectRelationshipModel> GetBySubjectID(int subjectID);
        public List<SubjectRelationshipModel> GetRelatedSubjects(SubjectModel subject);
        public List<SubjectRelationshipModel> GetSubjectRelationshipsBySubject(int id);
    }
}
