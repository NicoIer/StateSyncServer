// See https://aka.ms/new-console-template for more information

using Network.Client;

TelepathyClientSocket socket = new TelepathyClientSocket();
NetworkClient client = new NetworkClient(socket,60);
client.AddSystem<NetworkClientTime>();

UriBuilder uriBuilder = new UriBuilder
{
    Host = "localhost",
    Port = 8080,
};

await client.Run(uriBuilder.Uri);