namespace StudentManagement.Models
{
    public class RemoteStudent : Student
    {
        private string Location { get; set; }

        public RemoteStudent(string name, string studentId, string location) : base(name, studentId)
        {
            Location = location;
        }

        public override void ShowInfo()
        {
            Console.WriteLine($"Name: {Name}, StudentID: {StudentID}, From: {Location}");
        }
    }
}
