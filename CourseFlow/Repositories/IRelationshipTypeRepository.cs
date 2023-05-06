using CourseFlow.Models;
using System.Collections.Generic;

namespace CourseFlow.Repositories
{
    public interface IRelationshipTypeRepository
    {
        void Add(RelationshipTypeModel relationshipTypeModel);
        void Edit(RelationshipTypeModel relationshipTypeModel);
        void Remove(int id);
        RelationshipTypeModel GetById(int id);
        IEnumerable<RelationshipTypeModel> GetAll();
    }
}
