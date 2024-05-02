using System.IO;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class GameServerBuilder
{
    [MenuItem("Build/Build Linux Game Server")]
    public static void BuildLinuxGameServer()
    {
        string buildDirectory = Path.Combine("Builds", "GameServer");
        if (!Directory.Exists(buildDirectory))
            Directory.CreateDirectory(buildDirectory);
        PlayerSettings.SetScriptingBackend(BuildTargetGroup.Standalone, ScriptingImplementation.IL2CPP);
        PlayerSettings.SetApiCompatibilityLevel(BuildTargetGroup.Standalone, ApiCompatibilityLevel.NET_2_0);
        var buildLocationPathName = Path.Combine(buildDirectory, "multiplayer_tower_defense.x86_64");
        BuildReport report = BuildPipeline.BuildPlayer(
            new BuildPlayerOptions
            {
                scenes = new[]
                {
                    "Assets/Scenes/MultiplayerTowerDefense.unity"
                },
                locationPathName = buildLocationPathName,
                target = BuildTarget.StandaloneLinux64,
                options =  BuildOptions.Development,
                subtarget = (int)StandaloneBuildSubtarget.Server
            }
        );
        if (report.summary.result != BuildResult.Succeeded)
        {
            Debug.LogError($"{report.summary.result} Server build failed with the following details: {report.summary}");
            return;
        }
        Debug.Log("Server build succeeded and is located at: " + buildLocationPathName);
    }
}
