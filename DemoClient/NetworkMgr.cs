using System.Diagnostics;
using Network;
using Network.Client;

namespace DemoClient
{
    public class NetworkMgr : IClientSystem
    {
        public NetworkEntityMgr selfEntityMgr;
        public Dictionary<int, NetworkEntityMgr> otherEntityMgrs;

        /// <summary>
        /// 所有的网络实体 自己的和其他人的
        /// </summary>
        public Dictionary<uint, NetworkEntity> entities;

        public int connectionId => _client.connectionId;
        private NetworkClient _client;

        /// <summary>
        /// 针对不同类型的NetworkComponent的序列化器
        /// </summary>
        public NetworkComponentSerializer componentSerializer { get; private set; }


        public NetworkMgr()
        {
            selfEntityMgr = new NetworkEntityMgr();
            otherEntityMgrs = new Dictionary<int, NetworkEntityMgr>();
            entities = new Dictionary<uint, NetworkEntity>();
            componentSerializer = new NetworkComponentSerializer();
        }


        public void OnInit(NetworkClient t)
        {
            _client = t;
            _client.AddMsgHandler<NetworkEntitySpawn>(OnEntitySpawn);
            componentSerializer.Register<TransformComponent>();
        }

        private void OnEntitySpawn(NetworkEntitySpawn spawn)
        {
            Debug.Assert(connectionId != 0, "connectionId!=0");
            Debug.Assert(spawn.id != null, "spawn.id != null");
            Debug.Assert(spawn.owner != null, "spawn.owner != null");
            NetworkEntity entity = NetworkEntity.From(spawn.id.Value, spawn.owner.Value, spawn, componentSerializer);
            entities.Add(entity.id, entity);
            if (spawn.owner == connectionId)
            {
                NetworkLogger.Info($"服务器为自己生成了一个实体:{entity}");
                selfEntityMgr.Add(entity);
            }
            else
            {
                if (!otherEntityMgrs.ContainsKey(spawn.owner.Value))
                {
                    otherEntityMgrs.Add(spawn.owner.Value, new NetworkEntityMgr());
                }
                NetworkLogger.Info($"服务器为其他人[{spawn.owner}]生成了一个实体:{entity}");
                otherEntityMgrs[spawn.owner.Value].Add(entity);
            }
        }

        public void Dispose()
        {
        }
    }
}