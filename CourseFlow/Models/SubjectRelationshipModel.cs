namespace CourseFlow.Models
{
    public class SubjectRelationshipModel : AuditableEntity
    {
        public int RelationshipID { get; set; }
        public int SubjectID { get; set; }
        public int RelatedSubjectID { get; set; }
        public string RelationshipType { get; set; } // Pre-requisite, Co-requisite, Post-requisite
    }
}
