namespace CourseFlow.Models
{
    public class CourseModel : AuditableEntity
    {
        public int CourseID { get; set; }  
        public string CourseName { get; set; }
    }
}
