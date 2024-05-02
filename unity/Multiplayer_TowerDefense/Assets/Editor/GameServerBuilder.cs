using System.IO;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace Editor
{
    public static class GameServerBuilder
    {
        private static readonly string[] GameScenes =
        {
            "Assets/Scenes/MultiplayerTowerDefense.unity"
        };
        private static readonly string LinuxGameServerBuildDirectory = Path.Combine("Builds", "GameServer");
        private static readonly string LinuxGameServerBin = "multiplayer_tower_defense.x86_64";
[MenuItem("Build/Build Linux Game Server")]
public static void BuildLinuxGameServer()
{
    if (!Directory.Exists(LinuxGameServerBuildDirectory))
        Directory.CreateDirectory(LinuxGameServerBuildDirectory);
    var report = BuildPipeline.BuildPlayer(
        new BuildPlayerOptions
        {
            scenes = GameScenes,
            locationPathName = Path.Combine(LinuxGameServerBuildDirectory, LinuxGameServerBin),
            target = BuildTarget.StandaloneLinux64,
            subtarget = (int)StandaloneBuildSubtarget.Server,
            options =  BuildOptions.Development // currently this is the only way to see logging(?)
        }
    );
    if (report.summary.result != BuildResult.Succeeded)
    {
        Debug.LogError($"{report.summary.result} Server build failed with the following details: {report.summary}");
        return;
    }
    Debug.Log("Server build succeeded and is located at: " + Path.Combine(LinuxGameServerBuildDirectory, LinuxGameServerBin));
}
    }
}
