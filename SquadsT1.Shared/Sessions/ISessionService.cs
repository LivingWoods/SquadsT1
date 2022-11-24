using System.ServiceModel;
using ProtoBuf.Grpc;

namespace SquadsT1.Shared.Sessions;

[ServiceContract]
public interface ISessionService
{
    [OperationContract]
    Task<IEnumerable<SessionReply.IndexReply>> GetAllSessionsAsync(SessionRequest.IndexRequest request, CallContext context = default);
}
