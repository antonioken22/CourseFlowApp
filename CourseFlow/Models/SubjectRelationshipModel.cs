namespace CourseFlow.Models
{
    public class SubjectRelationshipModel : AuditableEntity
    {
        public int SubjectID { get; set; }
        public int RelatedSubjectID { get; set; }
        public int RelationshipTypeID { get; set; }

        // Navigation properties
        public SubjectModel Subject { get; set; }
        public SubjectModel RelatedSubject { get; set; }
        public RelationshipTypeModel RelationshipType { get; set; }
    }
}
