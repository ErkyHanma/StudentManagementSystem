namespace StudentManagement.Models
{
    public class Grade
    {
        public string Type { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double Score { get; set; }

        public Grade(string type, string description, double score)
        {
            Type = type;
            Description = description;
            Score = score;
        }
    }
}
