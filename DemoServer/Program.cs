// See https://aka.ms/new-console-template for more information

using DemoServer;
using Network.Server;

TelepathyServerSocket socket = new TelepathyServerSocket();
socket.port = 8080;
NetworkServer server = new NetworkServer(socket,240);
server.AddSystem<NetworkServerTime>();
server.AddSystem<DemoMgr>();



await server.Run();
