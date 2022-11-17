namespace SquadsT1.Shared.Sessions;

public abstract class SessionDto
{
    public class Index
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int SpotsLeft { get; set; }
        public string SessionType { get; set; } = default!;
        public string Trainer { get; set; } = default!;
    }
}
