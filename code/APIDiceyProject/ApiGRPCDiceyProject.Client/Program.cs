// The port number must match the port of the gRPC server.
using ApiGRPCDiceyProject.Client;
using Grpc.Net.Client;

using var channel = GrpcChannel.ForAddress("https://localhost:7042");
var client = new ThrowService.ThrowServiceClient(channel);
var reply = await client.GetThrowByIdAsync(
                  new Request() { SearchId = "1" });


Console.WriteLine("Retrieved throw: " + reply.ThrowId + " resultat : " + reply.Result);
Console.WriteLine("Press any key to exit...");
Console.ReadKey();