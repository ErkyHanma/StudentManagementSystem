namespace StudentManagement.Models
{
    public class InPersonStudent : Student
    {
        private string ClassRoom { get; set; }

        public InPersonStudent(string studentId, string name, string classRoom) : base(name, studentId)
        {
            ClassRoom = classRoom;
        }

        public override void ShowInfo()
        {
            Console.WriteLine($"Name: {Name}, StudentID: {StudentID}, Classroom: {ClassRoom}");
        }

    }
}
