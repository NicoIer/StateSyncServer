using System.Diagnostics;
using MemoryPack;
using Network;

namespace ToolkitTest;

public class NetworkPackerTests
{
    NetworkBuffer componentBuffer = new NetworkBuffer();
    NetworkBuffer msgBuffer = new NetworkBuffer();
    NetworkBuffer finalBuffer = new NetworkBuffer();

    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        Stopwatch stopwatch = new Stopwatch();
        int totalCount = 100000;// 测试是0.6732s
        TransformComponent transformComponent = new TransformComponent();

        var random = new Random(DateTime.Now.Millisecond);
        long defaultSizeTotal = 0;
        long compressedSizeTotal = 0;
        stopwatch.Start();
        for (int i = 0; i < totalCount; i++)
        {
            transformComponent.mask = TransformComponent.Mask.Pos | TransformComponent.Mask.Rotation |
                                      TransformComponent.Mask.Scale;
            transformComponent.pos = new Vector3(random.Next(int.MinValue, int.MaxValue),
                random.Next(int.MinValue, int.MaxValue), random.Next(int.MinValue, int.MaxValue));
            transformComponent.rotation = new Quaternion(random.Next(int.MinValue, int.MaxValue),
                random.Next(int.MinValue, int.MaxValue), random.Next(int.MinValue, int.MaxValue),
                random.Next(int.MinValue, int.MaxValue));
            transformComponent.scale = new Vector3(random.Next(int.MinValue, int.MaxValue),
                random.Next(int.MinValue, int.MaxValue), random.Next(int.MinValue, int.MaxValue));

            // int defaultSize = TestComponentSize(transformComponent);
            int compressedSize = TestComponentCompressedSize(transformComponent);
            // defaultSizeTotal += defaultSize;
            compressedSizeTotal += compressedSize;
        }

        var time = stopwatch.Elapsed;
        stopwatch.Stop();
        // Console.WriteLine(
        //     $"默认包大小: {(float)defaultSizeTotal / totalCount}, 压缩包大小: {(float)compressedSizeTotal / totalCount}, 耗时: {time},totalCount: {totalCount}");
    }


    // private int TestComponentSize(INetworkComponent component)
    // {
    //     componentBuffer.Reset();
    //     msgBuffer.Reset();
    //     finalBuffer.Reset();
    //
    //     NetworkComponentPacket componentPacket = component.ToDummyPacket(componentBuffer);
    //     NetworkPacker.Pack(componentPacket, msgBuffer, finalBuffer);
    //     NetworkPacker.Unpack(finalBuffer, out NetworkPacket unpackedPacket);
    //     NetworkComponentPacket unpackedComponentPacket =
    //         MemoryPackSerializer.Deserialize<NetworkComponentPacket>(unpackedPacket.payload);
    //     component.UpdateFromPacket(in unpackedComponentPacket);
    //
    //
    //     // Console.WriteLine(
    //     //     $"默认包大小: {finalBuffer.Position}, {component.GetType().Name}大小: {componentBuffer.Position}, NetworkComponentPacket包大小: {msgBuffer.Position}");
    //     return finalBuffer.Position;
    // }

    private int TestComponentCompressedSize(INetworkComponent component)
    {
        componentBuffer.Reset();
        msgBuffer.Reset();
        finalBuffer.Reset();

        NetworkComponentPacket componentPacket = component.ToDummyPacket(componentBuffer);
        NetworkPacker.PackCompressed(componentPacket, msgBuffer, finalBuffer);
        bool compressed =NetworkPacker.UnpackCompressed(finalBuffer, out var unpackedPacket);
        Assert.IsTrue(compressed);
        // NetworkComponentPacket unpackedComponentPacket =
        //     MemoryPackSerializer.Deserialize<NetworkComponentPacket>(unpackedPacket.payload);
        // component.UpdateFromPacket(in unpackedComponentPacket);


        // Console.WriteLine(
        //     $"压缩包大小: {finalBuffer.Position}, {component.GetType().Name}大小: {componentBuffer.Position}, NetworkComponentPacket包大小: {msgBuffer.Position}");
        return finalBuffer.Position;
    }
}