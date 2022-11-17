using System.ServiceModel;
using ProtoBuf.Grpc;

namespace SquadsT1.Shared.Sessions;

[ServiceContract]
public interface ISessionService
{
    [OperationContract]
    Task<IEnumerable<SessionReply.Index>> GetAllSessionsAsync(SessionRequest.Index request, CallContext context = default);
}
