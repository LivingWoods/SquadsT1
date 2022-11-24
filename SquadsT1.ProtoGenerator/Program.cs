using ProtoBuf;
using ProtoBuf.Grpc.Reflection;
using ProtoBuf.Meta;
using SquadsT1.Shared.Sessions;

SchemaGenerator generator = new SchemaGenerator()
{
    ProtoSyntax = ProtoSyntax.Proto3,

};

var schema = generator.GetSchema<ISessionService>();

using (var writer = new StreamWriter("C:\\Development Files\\Projects\\Personal\\SquadsT1\\SquadsT1.ProtoGenerator\\Protos\\session-services.proto"))
{
    await writer.WriteAsync(schema);
}

Console.WriteLine(Serializer.GetProto<SessionReply.IndexReply>());