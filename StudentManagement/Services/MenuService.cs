using StudentManagement.Models;

namespace StudentManagement.Services
{
    public class MenuService
    {
        private Teacher _teacher = null!;

        public void Init()
        {
            Console.Clear();
            Console.WriteLine("╔═══════════════════════════════════════╗");
            Console.WriteLine("║    ITLA Student Management System     ║");
            Console.WriteLine("╚═══════════════════════════════════════╝");
            Console.Write("\n  Type your name (teacher): ");
            string name = Console.ReadLine()?.Trim() ?? "Teacher";
            _teacher = new Teacher(name);

            Console.Clear();
            Console.WriteLine("╔═══════════════════════════════════════╗");
            Console.WriteLine("║    ITLA Student Management System     ║");
            Console.WriteLine("╚═══════════════════════════════════════╝");
            Console.WriteLine($"\n  Welcome, {name}!\n");
            PauseAndContinue();

            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                ShowMainMenu();
                string option = Console.ReadLine()?.Trim() ?? "";

                switch (option)
                {
                    case "1": ManageSubjects(); break;
                    case "2": ManageGroups(); break;
                    case "3": ManageStudents(); break;
                    case "4": RegisterGrade(); break;
                    case "5": ViewReports(); break;
                    case "0": exit = true; break;
                    default:
                        Alert("Invalid option. Please try again.");
                        PauseAndContinue();
                        break;
                }
            }

            Console.Clear();
            Console.WriteLine("\n  Goodbye. See you next time!\n");
        }

        // MAIN MENU
        private void ShowMainMenu()
        {
            Console.WriteLine("╔═══════════════════════════════════════╗");
            Console.WriteLine("║    ITLA Student Management System     ║");
            Console.WriteLine("╠═══════════════════════════════════════╣");
            Console.WriteLine($"║  Teacher: {_teacher.Name,-29}║");
            Console.WriteLine("╠═══════════════════════════════════════╣");
            Console.WriteLine("║  [1]  Manage subjects                 ║");
            Console.WriteLine("║  [2]  Manage groups                   ║");
            Console.WriteLine("║  [3]  Manage students                 ║");
            Console.WriteLine("║  [4]  Register grade                  ║");
            Console.WriteLine("║  [5]  View reports                    ║");
            Console.WriteLine("║  [0]  Exit                            ║");
            Console.WriteLine("╚═══════════════════════════════════════╝");
            Console.Write("\n  Choose an option: ");
        }

        // SUBJECTS
        private void ManageSubjects()
        {
            Console.Clear();
            PrintSectionHeader("Manage Subjects");
            Console.WriteLine("  [1] Add subject");
            Console.WriteLine("  [2] List subjects");
            Console.WriteLine("  [3] Back");
            Console.Write("\n  Option: ");
            string op = Console.ReadLine()?.Trim() ?? "";

            Console.WriteLine();

            if (op == "1")
            {
                Console.Write("  Subject name: ");
                string name = Console.ReadLine()?.Trim() ?? "";
                var result = _teacher.AddSubject(name);
                Respond(result);
            }
            else if (op == "2")
            {
                _teacher.ShowSubjects();
            }
            else if (op == "3")
            {
                return;
            }

            PauseAndContinue();
        }

        // GROUPS
        private void ManageGroups()
        {
            Console.Clear();
            PrintSectionHeader("Manage Groups");
            var subject = SelectSubject();
            if (subject == null) return;

            Console.WriteLine();
            Console.WriteLine("  [1] Add group");
            Console.WriteLine("  [2] List groups");
            Console.WriteLine("  [3] Back");
            Console.Write("\n  Option: ");
            string op = Console.ReadLine()?.Trim() ?? "";

            Console.WriteLine();

            if (op == "1")
            {
                Console.Write("  Group name (e.g. Group A): ");
                string groupName = Console.ReadLine()?.Trim() ?? "";
                var result = subject.AddGroup(groupName);
                Respond(result);
            }
            else if (op == "2")
            {
                subject.ShowGroups();
            }
            else if (op == "3")
            {
                return;
            }

            PauseAndContinue();
        }

        // STUDENTS
        private void ManageStudents()
        {
            Console.Clear();
            PrintSectionHeader("Manage Students");
            var group = SelectGroup();
            if (group == null) return;

            Console.WriteLine();
            Console.WriteLine("  [1] Add student");
            Console.WriteLine("  [2] Back");
            Console.Write("\n  Option: ");
            string op = Console.ReadLine()?.Trim() ?? "";

            Console.WriteLine();

            if (op == "1") AddStudentToGroup(group);
            else if (op == "2") return;

            PauseAndContinue();
        }

        private void AddStudentToGroup(Group group)
        {
            Console.Write("  Full name: ");
            string name = Console.ReadLine()?.Trim() ?? "";
            Console.Write("  Student ID: ");
            string studentId = Console.ReadLine()?.Trim() ?? "";

            Console.WriteLine();
            Console.WriteLine("  Student type:");
            Console.WriteLine("    [1] In-person");
            Console.WriteLine("    [2] Remote");
            Console.Write("\n  Type: ");
            string type = Console.ReadLine()?.Trim() ?? "";

            Console.WriteLine();
            Student? student = null;

            if (type == "1")
            {
                Console.Write("  Classroom (e.g. 203-B): ");
                string classroom = Console.ReadLine()?.Trim() ?? "No classroom";
                student = new InPersonStudent(studentId, name, classroom);
            }
            else if (type == "2")
            {
                Console.Write("  Location (e.g. Dominican Republic): ");
                string location = Console.ReadLine()?.Trim() ?? "N/A";
                student = new RemoteStudent(name, studentId, location);
            }
            else
            {
                Alert("Invalid student type.");
                return;
            }

            var result = group.AddStudent(student);
            Respond(result);
        }

        // GRADES
        private void RegisterGrade()
        {
            Console.Clear();
            PrintSectionHeader("Register Grade");
            var group = SelectGroup();
            if (group == null) return;

            Console.WriteLine();
            Console.Write("  Student ID: ");
            string studentId = Console.ReadLine()?.Trim() ?? "";

            var search = group.FindStudent(studentId);
            if (!search.IsSuccess || !search.Data)
            {
                Alert(search.Message);
                PauseAndContinue();
                return;
            }

            Student student = !search.Data;

            Console.WriteLine();
            Console.WriteLine("  Grade type:");
            Console.WriteLine("    [1] Exam");
            Console.WriteLine("    [2] Practice");
            Console.WriteLine("    [3] Assignment");
            Console.Write("\n  Type: ");
            string typeNum = Console.ReadLine()?.Trim() ?? "";
            string gradeType = typeNum == "1" ? "Exam" : typeNum == "2" ? "Practice" : "Assignment";

            Console.WriteLine();
            Console.Write("  Description (e.g. First partial): ");
            string description = Console.ReadLine()?.Trim() ?? "No description";

            double score = ReadDoubleInRange("\n  Score (0–100): ", 0, 100);

            var grade = new Grade(gradeType, description, score);
            var result = student.AddGrade(grade);
            Respond(result);
            PauseAndContinue();
        }

        // REPORTS
        private void ViewReports()
        {
            Console.Clear();
            PrintSectionHeader("Reports");
            Console.WriteLine("  [1] Grade list by group");
            Console.WriteLine("  [2] Full summary");
            Console.WriteLine("  [3] Back");
            Console.Write("\n  Option: ");
            string op = Console.ReadLine()?.Trim() ?? "";

            Console.WriteLine();

            if (op == "1")
            {
                var group = SelectGroup();
                group?.ShowGradeList();
            }
            else if (op == "2")
            {
                _teacher.ShowFullSummary();
            }
            else if (op == "3")
            {
                return;
            }

            PauseAndContinue();
        }

        // HELPERS
        private Subject? SelectSubject()
        {
            var subjects = _teacher.Subjects;
            if (subjects.Count == 0)
            {
                Alert("No subjects registered. Please add one first.");
                PauseAndContinue();
                return null;
            }

            Console.WriteLine("  Available subjects:");
            Console.WriteLine("  ─────────────────────────────────────");
            for (int i = 0; i < subjects.Count; i++)
                Console.WriteLine($"    [{i + 1}] {subjects[i].Name}  ({subjects[i].SubjectId})");
            Console.WriteLine("  ─────────────────────────────────────");

            int idx = ReadIntInRange("\n  Select subject (number): ", 1, subjects.Count) - 1;
            return subjects[idx];
        }

        private Group? SelectGroup()
        {
            var subject = SelectSubject();
            if (subject == null) return null;

            var groups = subject.Groups;
            if (groups.Count == 0)
            {
                Alert("No groups in this subject. Please add one first.");
                PauseAndContinue();
                return null;
            }

            Console.WriteLine();
            Console.WriteLine("  Available groups:");
            Console.WriteLine("  ─────────────────────────────────────");
            for (int i = 0; i < groups.Count; i++)
                Console.WriteLine($"    [{i + 1}] {groups[i].Name}");
            Console.WriteLine("  ─────────────────────────────────────");

            int idx = ReadIntInRange("\n  Select group (number): ", 1, groups.Count) - 1;
            return groups[idx];
        }

        // UTILITIES
        private void PrintSectionHeader(string title)
        {
            string padded = $"  {title}  ";
            string bar = new string('─', padded.Length);
            Console.WriteLine($"  ┌{bar}┐");
            Console.WriteLine($"  │{padded}│");
            Console.WriteLine($"  └{bar}┘");
            Console.WriteLine();
        }

        private void Respond(OperationResult result)
        {
            string icon = result.IsSuccess ? "✓" : "✗";
            Console.WriteLine($"\n  {icon}  {result.Message}");
        }

        private void Alert(string message)
            => Console.WriteLine($"\n  ⚠  {message}");

        private void PauseAndContinue()
        {
            Console.Write("\n  Press Enter to return to menu...");
            Console.ReadLine();
            // Console.Clear() is called at the top of the while loop in Init()
        }

        private int ReadIntInRange(string message, int min, int max)
        {
            while (true)
            {
                Console.Write(message);
                if (int.TryParse(Console.ReadLine(), out int value) && value >= min && value <= max)
                    return value;
                Console.WriteLine($"\n  ⚠  Please enter a number between {min} and {max}.\n");
            }
        }

        private double ReadDoubleInRange(string message, double min, double max)
        {
            while (true)
            {
                Console.Write(message);
                if (double.TryParse(Console.ReadLine(), out double value) && value >= min && value <= max)
                    return value;
                Console.WriteLine($"\n  ⚠  Please enter a value between {min} and {max}.\n");
            }
        }
    }
}