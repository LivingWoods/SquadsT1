using Grpc.Net.Client;
using ProtoBuf.Grpc.Client;
using SquadsT1.Shared.Sessions;

namespace GrpcGreeterClient;

internal class Program
{
    private static async Task Main(string[] args)
    {
        using var channel = GrpcChannel.ForAddress("http://localhost:25152");
        var sessionClient = channel.CreateGrpcService<ISessionService>();

        var reply = await sessionClient.GetAllSessionsAsync(new SessionRequest.IndexRequest());

        foreach (var session in reply)
        {
            Console.WriteLine($"Greeting: {session?.Trainer} - {session?.SessionType}");
        }
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}