namespace StudentManagement.Models
{
    public class Subject
    {
        public Guid SubjectId { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<Group> Groups { get; private set; } = [];

        public Subject(string name)
        {
            SubjectId = Guid.NewGuid();
            Name = name;
        }


        public OperationResult AddGroup(string groupName)
        {
            if (Groups.Any(c => c.Name == groupName))
                return OperationResult.Fail($"Group with name '{groupName}' already exists.");

            Groups.Add(new Group(groupName));
            return OperationResult.Success($"Group '{groupName}' added to the subject.");
        }

        public void ShowGroups()
        {
            if (Groups.Count == 0)
            {
                Console.WriteLine("You don't groups assigned to this subject.");
                return;
            }
            Console.WriteLine($"Groups for subject {Name}:");
            foreach (var group in Groups)
            {
                Console.WriteLine($"- {group.Name}");
            }
        }


    }
}
