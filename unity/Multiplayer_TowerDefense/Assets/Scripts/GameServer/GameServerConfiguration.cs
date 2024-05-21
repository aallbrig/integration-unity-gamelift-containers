using System;
using UnityEngine;

namespace GameServer
{
    [Serializable]
    public class EnvVarReader
    {
        public string envVarName;
        public EnvVarReader(string envVarName) => this.envVarName = envVarName;
        public string Value() => Environment.GetEnvironmentVariable(envVarName);
        public override string ToString() => $"{envVarName}: {Value()}";
    }
    [CreateAssetMenu(fileName = "GameServerConfiguration", menuName = "Game/new Game Server Configuration", order = 0)]
    public class GameServerConfiguration : ScriptableObject
    {
        #if UNITY_SERVER
        public EnvVarReader fleetId = new EnvVarReader("AWS_GAMELIFT_FLEET_ID");
        public EnvVarReader aliasId = new EnvVarReader("AWS_GAMELIFT_ALIAS_ID");
        private void Awake()
        {
            Debug.Log($"{name} | on awake");
        }
        private void OnEnable()
        {
            Debug.Log($"{name} | on enable");
            Debug.Log($"{name} | {fleetId}");
            Debug.Log($"{name} | {aliasId}");
        }
        private void OnDisable()
        {
            Debug.Log($"{name} | on disable");
        }
        #endif
    }
}
