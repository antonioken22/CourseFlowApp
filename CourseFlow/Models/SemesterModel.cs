namespace CourseFlow.Models
{
    public class SemesterModel : AuditableEntity
    {
        public int SemesterID { get; set; }
        public string Semester { get; set; }
    }
}
