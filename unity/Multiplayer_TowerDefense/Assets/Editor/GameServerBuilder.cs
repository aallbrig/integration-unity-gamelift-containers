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
        private static readonly string LinuxGameServerBuildDirectory = "GameServer";
        private static readonly string LinuxGameServerBin = "multiplayer_tower_defense.x86_64";
        private static readonly string ProdLinuxGameServerBuildDirectory = Path.Combine("Builds", "Prod", LinuxGameServerBuildDirectory);
        private static readonly string DevLinuxGameServerBuildDirectory = Path.Combine("Builds", "Dev", LinuxGameServerBuildDirectory);
        [MenuItem("Build/Build Linux Game Server (Dev)")]
        public static void BuildLinuxGameServerDev()
        {
            if (!Directory.Exists(DevLinuxGameServerBuildDirectory))
                Directory.CreateDirectory(DevLinuxGameServerBuildDirectory);
            var report = BuildPipeline.BuildPlayer(
                new BuildPlayerOptions
                {
                    scenes = GameScenes,
                    locationPathName = Path.Combine(DevLinuxGameServerBuildDirectory, LinuxGameServerBin),
                    target = BuildTarget.StandaloneLinux64,
                    subtarget = (int)StandaloneBuildSubtarget.Server,
                    options =  BuildOptions.Development // currently this is the only way to see logging(?) on my m2 macbook (same build runs w/ logging on intel based aws ec2 instance)
                }
            );
            if (report.summary.result != BuildResult.Succeeded)
            {
                Debug.LogError($"{report.summary.result} Server build failed with the following details: {report.summary}");
                return;
            }
            Debug.Log("Server build succeeded and is located at: " + Path.Combine(LinuxGameServerBuildDirectory, LinuxGameServerBin));
        }
        [MenuItem("Build/Build Linux Game Server (Prod)")]
        public static void BuildLinuxGameServerProd()
        {
            if (!Directory.Exists(ProdLinuxGameServerBuildDirectory))
                Directory.CreateDirectory(ProdLinuxGameServerBuildDirectory);
            var report = BuildPipeline.BuildPlayer(
                new BuildPlayerOptions
                {
                    scenes = GameScenes,
                    locationPathName = Path.Combine(ProdLinuxGameServerBuildDirectory, LinuxGameServerBin),
                    target = BuildTarget.StandaloneLinux64,
                    subtarget = (int)StandaloneBuildSubtarget.Server
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
