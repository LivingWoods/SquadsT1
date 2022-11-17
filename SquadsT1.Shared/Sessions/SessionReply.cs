using System.Runtime.Serialization;

namespace SquadsT1.Shared.Sessions;

public abstract class SessionReply
{
    [DataContract]
    public class Index
    {
        [DataMember(Order = 1)] public int Id { get; set; }
        [DataMember(Order = 2)] public DateTime StartDate { get; set; }
        [DataMember(Order = 3)] public DateTime EndDate { get; set; }
        [DataMember(Order = 4)] public int SpotsLeft { get; set; }
        [DataMember(Order = 5)] public string SessionType { get; set; } = default!;
        [DataMember(Order = 6)] public string Trainer { get; set; } = default!;
    }
}
