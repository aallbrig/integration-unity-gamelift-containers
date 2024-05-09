using Mirror;
using UnityEngine;

namespace GameServer
{
    public class MultiplayerNetworkManager : NetworkManager
    {
        public GameServerConfiguration config;
        public override void OnStartServer()
        {
            Debug.Log($"{name} | On Start Server");
        }
    }
}
