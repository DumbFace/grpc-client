
using Common.Models.BaseModels;
using Grpc.Core;
using Grpc.Net.Client;
using GrpcEmployee;
using GrpcGreeter;
using GrpcUser;
using Newtonsoft.Json;
using Test;

namespace grpc_client;

class Program
{

    static async Task Main(string[] args)
    {

        string path = "C:\\Users\\KangFarm\\bearer.txt";
        string token = String.Empty;
        if (File.Exists(path))
        {
            string content = File.ReadAllText(path);
            // Console.WriteLine("File Content:\n" + content);
            if (!String.IsNullOrEmpty(content) && content.StartsWith("Authorization: Bearer "))
            {
                token = content.Substring("Authorization: Bearer ".Length).Trim();
            }

        }
        // // Create Metadata with Authorization Header
        var metadata = new Metadata
        {
            { "Authorization", $"Bearer {token}" }
        };

        var channel = GrpcChannel.ForAddress("https://localhost:5000");
        var greetClient = new Greeter.GreeterClient(channel);
        var employeeClient = new GrpcServiceEmployee.GrpcServiceEmployeeClient(channel);
        var serviceClient = new GrpcServiceUser.GrpcServiceUserClient(channel);

        try
        {

            // Request GreetServices
            // ----------------------------------------------
            // var reply = await greetClient.SayHelloAsync(new HelloRequest { Name = "Test" }, metadata);
            // if (reply is not null)
            // {
            //     Console.WriteLine($"{reply}");
            // }

            // Request EmployeeService
            // ----------------------------------------------
            // var reply = await employeeClient.GetEmployeesAsync(new GrpcEmployee.Empty());
            // if (reply.Employees.Any())
            // {
            //     Console.WriteLine($"Count: {reply.Employees}");
            // }


            //Request UserService
            //------------------------------------------------
            // var response = await serviceClient.GetUsersAsync(new GrpcUser.Empty());
            // if (response.Users.Any())
            // {
            //     Console.WriteLine($"Count: {response.Users.Count}");
            // }

            var call = serviceClient.GetUserAsync(new GetUserRequest() { UserId = 999999999 });

            try
            {
                var response = await call.ResponseAsync;
                var status = call.GetStatus(); // Lấy status code
                Console.WriteLine($"gRPC Status: {status.StatusCode}");
                if (status.StatusCode == StatusCode.OK)
                {
                    // var dataAsJson = response.Response.Unpack<StringValue>().ToString();
                    var data = JsonConvert.DeserializeObject<ApiResponseModel<GetUserResponseModel>>(response.Response);
                    Console.WriteLine($"Response: {data.Data}");
                }
            }
            catch (RpcException ex)
            {
                Console.WriteLine($"Error: {ex.Status}");
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ex: {ex.Message}");
        }

        Console.WriteLine("Press any key to exit...");
    }
}
