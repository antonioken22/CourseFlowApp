namespace CourseFlow.Models
{
    public class SubjectModel : AuditableEntity
    {
        public int SubjectID { get; set; }
        public string SubjectCode { get; set; }
        public string SubjectName { get; set; }
        public int CourseID { get; set; }
        public int AcademicYearID { get; set; }
        public int YearLevelID { get; set; }
        public int SemesterID { get; set; }
    }
}
