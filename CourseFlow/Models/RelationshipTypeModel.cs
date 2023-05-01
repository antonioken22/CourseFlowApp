namespace CourseFlow.Models
{
    public class RelationshipTypeModel : AuditableEntity
    {
        public string RelationshipType { get; set; } // Pre-requisite, Co-requisite, and Post-requisite
    }
}
