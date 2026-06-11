namespace StudentManagement.Models
{
    public abstract class Student
    {
        public string Name { get; protected set; }
        public string StudentID { get; protected set; }
        protected Grade? Grade { get; set; }


        protected Student(string name, string studentID, Grade? grade = null)
        {
            Name = name;
            StudentID = studentID;
            Grade = grade;
        }

        public virtual void ShowInfo()
        {
            Console.WriteLine($"Name: {Name}, StudentID: {StudentID}");
            if (Grade != null)
            {
                Console.WriteLine($"Score Type: {Grade.Type}, Description: {Grade.Description}, Total Score: {Grade.Score}");
            }
        }

        public OperationResult AddGrade(Grade grade)
        {
            if (grade.Score < 0 || grade.Score > 100)
                return OperationResult.Fail("The score should be between 0 and 100");

            Grade = grade;
            return OperationResult.Success($"Grade '{grade.Description}' register.");
        }


        public virtual void ShowGrade()
        {
            ShowInfo();
            if (Grade == null)
            {
                Console.WriteLine("Not grade were register");
                return;
            }

            Console.WriteLine($"{Grade.Score}");
        }

        public bool Approved() => Grade?.Score >= 70;

    }
}
