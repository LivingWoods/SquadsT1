using System.Runtime.Serialization;

namespace SquadsT1.Shared.Greetings;

[DataContract]
public class HelloReply
{
    [DataMember(Order = 1)]
    public string Message { get; set; } = default!;
}
