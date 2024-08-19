using System.Security.Authentication;
using System.Text;
using GameCore.Service;
using Grpc.Core;
using Grpc.Net.Client;
using MagicOnion.Client;

var httpClientHandler = new HttpClientHandler
{
    ServerCertificateCustomValidationCallback = (message, certificate2, arg3, arg4) => true  //忽略掉证书异常
};

GrpcChannelOptions options = new GrpcChannelOptions()
{
    HttpHandler = httpClientHandler
};


var channel = GrpcChannel.ForAddress("https://localhost:7121",options);
var client = MagicOnionClient.Create<IGameService>(channel);
var result = client.SumAsync(1, 2).ResponseAsync.Result;
Console.WriteLine(result);