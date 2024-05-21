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
        public override void OnClientConnect()
        {
            Debug.Log($"{name} | On Client Connect");
        }
        public override void OnClientDisconnect()
        {
            Debug.Log($"{name} | On Client Disconnect");
        }
    }
}
