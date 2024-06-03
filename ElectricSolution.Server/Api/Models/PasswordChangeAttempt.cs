namespace ElectricSolution.Server.Api.Models
{
    public class PasswordChangeAttempt
    {
        public int Attempts { get; set; } = 0;
        public DateTime LastAttemptTime { get; set; } = DateTime.MinValue;
    }
}
