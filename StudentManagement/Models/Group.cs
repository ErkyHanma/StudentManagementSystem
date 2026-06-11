namespace StudentManagement.Models
{
    public class Group
    {
        public string Name { get; set; } = string.Empty;
        private List<Student> Students { get; set; } = [];

        public Group(string name)
        {
            Name = name;
        }


        public OperationResult AddStudent(Student student)
        {
            if (Students.Any(s => s.StudentID == student.StudentID))
                return OperationResult.Fail($"Student with ID '{student.StudentID}' already exists in the group.");
            Students.Add(student);
            return OperationResult.Success($"Student '{student.Name}' added to the course.");
        }

        public void ShowStudents()
        {
            if (Students.Count == 0)
            {
                Console.WriteLine("No students enrolled in this group.");
                return;
            }
            Console.WriteLine($"Students enrolled in {Name}:");
            foreach (var student in Students)
            {
                student.ShowInfo();
                Console.WriteLine();
            }
        }

        public void ShowStudentScore()
        {
            if (Students.Count == 0)
            {
                Console.WriteLine("No students enrolled in this group.");
                return;
            }
            Console.WriteLine($"Students enrolled in {Name}:");
            foreach (var student in Students)
            {
                student.ShowGrade();
                Console.WriteLine();
            }
        }

        public double CalculateApprovedStudents()
        {
            if (Students.Count == 0)
                return 0;

            int approvedStudents = Students.Count(e => e.Approved());
            return (double)approvedStudents / Students.Count * 100;
        }


        public OperationResult FindStudent(string studentID)
        {
            var student = Students.FirstOrDefault(s => s.StudentID == studentID);
            if (student == null)
                return OperationResult.Fail($"Student with ID '{studentID}' not found in the group.");

            return OperationResult.Success($"Student with ID '{studentID}' found.", student);
        }


        public void ShowGradeList()
        {
            if (Students.Count == 0)
            {
                Console.WriteLine("No students enrolled in this group.");
                return;
            }

            Console.WriteLine($"Grade list for {Name}:");
            foreach (var student in Students)
            {
                Console.WriteLine($"Student: {student.Name}, ID: {student.StudentID}");
                student.ShowGrade();
                Console.WriteLine();
            }
        }

    }



}
