using Grpc.Core;
using GrpcGreeterServer;

namespace GrpcGreeterServer.Services;

public class GreeterService : Greeter.GreeterBase
{
    private readonly ILogger<GreeterService> _logger;
    public GreeterService(ILogger<GreeterService> logger)
    {
        _logger = logger;
    }

    public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
    {

        // C Sharp Language
        // var name = request.Name;
        // var message = "Welcome to Transflower dear " + name;
        return Task.FromResult(new HelloReply
        {
            //Bussiness Logic
            
            Message = "Welcome to Transflower dear " + request.Name
        });
    }
}
