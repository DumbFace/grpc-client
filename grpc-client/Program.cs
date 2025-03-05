using Grpc.Net.Client;
using GrpcGreeter;
using GrpcEmployee;
namespace grpc_client;

class Program
{
    static async Task Main(string[] args)
    {

        var channel = GrpcChannel.ForAddress("https://localhost:5000");
        var client = new GrpcServiceEmployee.GrpcServiceEmployeeClient(channel);
        try
        {
            var reply = await client.GetEmployeesAsync(new Empty());

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ex: {ex.Message}");
        }


        // var reply = await client.SayHelloAsync(
        //                   new HelloRequest { Name = "Damn" });
        // Console.WriteLine("Greeting: " + reply.Message);
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();

    }
}
