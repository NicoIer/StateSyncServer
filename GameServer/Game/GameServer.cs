using System.Collections.Concurrent;
using Network;
using Network.Server;

namespace Server.Game
{
    public class GameServer : NetworkMgr
    {
        public static readonly ConcurrentBag<GameServer> mgrs = new ConcurrentBag<GameServer>();
        public int ThreadId { get; private set; }
        public static event Action<GameServer> OnAddServer = delegate { };

        public override void OnInit(NetworkServer t)
        {
            base.OnInit(t);
            mgrs.Add(this);
            lock (OnAddServer)
            {
                OnAddServer(this);
            }
            ThreadId = Thread.CurrentThread.ManagedThreadId;
            componentSerializer.Register<TransformComponent>();
        }
    }
}