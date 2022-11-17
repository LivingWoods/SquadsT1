using Grpc.Net.Client;
using ProtoBuf.Grpc.Client;
using SquadsT1.Shared.Sessions;

namespace GrpcGreeterClient;

internal class Program
{
    private static async Task Main(string[] args)
    {
        using var channel = GrpcChannel.ForAddress("https://localhost:7152");
        var client = channel.CreateGrpcService<ISessionService>();

        var reply = await client.GetAllSessionsAsync(new SessionRequest.Index());

        foreach (var session in reply)
        {
            Console.WriteLine($"Greeting: {session?.Trainer} - {session?.SessionType}");
        }
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}