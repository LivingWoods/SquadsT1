using System.Runtime.Serialization;

namespace SquadsT1.Shared.Sessions;

public abstract class SessionRequest
{
    [DataContract]
    public class Index { }
}
