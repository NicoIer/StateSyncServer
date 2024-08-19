// See https://aka.ms/new-console-template for more information

using Network;
using Network.Server;

TelepathyServerSocket socket = new TelepathyServerSocket();
socket.port = 8080;
NetworkServer server = new NetworkServer(socket,240);
server.AddSystem<NetworkServerTime>();


await server.Run();
