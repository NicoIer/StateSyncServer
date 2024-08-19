using Network;
using Network.Server;

namespace DemoServer
{
    public class DemoMgr : NetworkMgr
    {
        public override void OnInit(NetworkServer t)
        {
            base.OnInit(t);
            componentSerializer.Register<TransformComponent>();
        }
    }   
}