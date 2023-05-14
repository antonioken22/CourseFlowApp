namespace CourseFlow.Models
{
    public class UserAccountModel : UserModel
    {
        public string DisplayName { get => $"{LastName}, {FirstName}"; }
        // public byte[] ProfilePicture { get; set; }
    }
}
