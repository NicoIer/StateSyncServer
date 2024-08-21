// // See https://aka.ms/new-console-template for more information
//
// using DemoClient;
// using Network;
// using Network.Client;
// using UnityEngine;
//
// TelepathyClientSocket socket = new TelepathyClientSocket();
// NetworkClient client = new NetworkClient(socket, 60);
// client.AddSystem<NetworkClientTime>();
//
// UriBuilder uriBuilder = new UriBuilder
// {
//     Host = "localhost",
//     Port = 8080,
// };
// var task = client.Run(uriBuilder.Uri);
// client.AddSystem<NetworkMgr>();
//
// bool connected = false;
// socket.OnConnected += () => { connected = true; };
//
// await Task.Run(async () =>
// {
//     while (!connected)
//     {
//         await Task.Delay(100);
//     }
// });
//
// NetworkBufferPool bufferPool = new NetworkBufferPool();
// NetworkComponentSerializer componentSerializer = client.GetSystem<NetworkMgr>().componentSerializer;
// TransformComponent transformComponent = new TransformComponent();
// transformComponent.pos = new Vector3(0, 0, 0);
// var buffer = bufferPool.Get();
// buffer.Reset();
// NetworkComponentPacket packet = transformComponent.ToDummyPacket(buffer);
// ArraySegment<NetworkComponentPacket> componentPackets = new ArraySegment<NetworkComponentPacket>(new[] { packet });
// NetworkEntitySpawn spawn = new NetworkEntitySpawn(null, client.connectionId, componentPackets);
//
// client.Send(spawn);
//
// await task;

Console.WriteLine("Hello, World!");