namespace CourseFlow.Models
{
    public class UserAccountModel : AuditableEntity
    {
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public byte[] ProfilePicture { get; set; }
    }
}
