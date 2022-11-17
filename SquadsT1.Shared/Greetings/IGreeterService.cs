using ProtoBuf.Grpc;
using System.ServiceModel;

namespace SquadsT1.Shared.Greetings;

[ServiceContract]
public interface IGreeterService
{
    [OperationContract]
    Task<HelloReply> SayHelloAsync(HelloRequest request, CallContext context = default);
}