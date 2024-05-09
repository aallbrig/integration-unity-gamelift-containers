using Aws.GameLift.Server;
using Mirror;
using UnityEngine;

public class GameLiftManager : MonoBehaviour
{
    // Environment Variable names to look in
    // If anywhere compute name
    public string anywhereComputeNameEnvVar = "AWS_GAMELIFT_OPTION_ANYWHERE_COMPUTE_NAME";
    public string fleetIdEnvVar = "AWS_GAMELIFT_OPTION_FLEET_ID";
    public string aliasIdEnvVar = "AWS_GAMELIFT_OPTION_ALIAS_ID";
    // Flag names to look for values
    public string anywhereComputeNameFlag= "compute-name";
    public string fleetIdFlag = "fleet-id";
    public string aliasIdFlag = "alias-id";
    [SerializeField]
    private NetworkManager sceneNetworkManager;
    private string _fleetId;
    private string _aliasId;
    private string _anywhereComputeName;
    private string _authId;
    void Start()
    {
        sceneNetworkManager ??= FindObjectOfType<NetworkManager>();
        if (sceneNetworkManager == null)
        {
            Debug.LogError($"{name} | no active network managers found");
            enabled = false;
            return;
        }
        TryObtainConfiguration();
        // if not configuration, return
        Debug.Log($"{name} | aws gamelift sdk version: {GameLiftServerAPI.GetSdkVersion()}");
        // GameLiftServerAPI.InitSDK(new ServerParameters());
    }
    private void TryObtainConfiguration()
    {
        // try get from command line
        TryGetFromCommandLine();
        // try get from ENV VAR
        TryGetFromEnvVars();
        // if config not available, deactivate game object
    }
    private void TryGetFromEnvVars()
    {
    }
    private void TryGetFromCommandLine()
    {
    }
}
