namespace StudentManagement.Models
{
    public class Teacher
    {
        public string Name { get; set; } = string.Empty;
        public List<Subject> Subjects { get; private set; } = [];

        public Teacher(string name)
        {
            Name = name;
        }


        public OperationResult AddSubject(string name)
        {
            if (Subjects.Any(s => s.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
                return OperationResult.Fail($"Subject with name '{name}' already exists.");
            Subjects.Add(new Subject(name));
            return OperationResult.Success($"Subject '{name}' added successfully.");
        }

        public void ShowSubjects()
        {
            if (Subjects.Count == 0)
            {
                Console.WriteLine("No subjects assigned to this teacher.");
                return;
            }
            Console.WriteLine($"Subjects taught by {Name}:");
            foreach (var subject in Subjects)
            {
                Console.WriteLine($"- {subject.Name}");
            }

        }

        public void ShowFullSummary()
        {
            Console.WriteLine($"Teacher: {Name}");

            if (Subjects.Count == 0)
            {
                Console.WriteLine("\nNo subjects assigned to this teacher.");
                return;
            }

            foreach (var subject in Subjects)
            {
                Console.WriteLine($"Subject {subject.Name}");
                subject.ShowGroups();
            }


        }
    }
}