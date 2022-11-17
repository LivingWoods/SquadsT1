using System.Runtime.Serialization;

namespace SquadsT1.Shared.Greetings;

[DataContract]
public class HelloRequest
{
    [DataMember(Order = 1)]
    public string Name { get; set; } = default!;
}
