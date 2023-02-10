// The port number must match the port of the gRPC server.
using ApiGRPCDiceyProject;
using Grpc.Net.Client;

var httpHandler = new HttpClientHandler();
httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
using var channel = GrpcChannel.ForAddress("https://localhost:7088", new GrpcChannelOptions { HttpHandler = httpHandler });
var client = new ThrowService.ThrowServiceClient(channel);

var searchedGuid = "cc6f9111-b174-4064-814b-ce7eb4169e80";
var reply = await client.GetThrowByIdAsync(
                  new RequestGetThrowById() { SearchId = searchedGuid });


Console.WriteLine($"Retrieved throw: {reply.ThrowId} with resultat : {reply.Result} with a dice with {reply.IdDice} faces" );

Console.WriteLine("Press any key to exit...");
Console.ReadKey();
