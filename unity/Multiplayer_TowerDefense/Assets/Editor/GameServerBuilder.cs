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
        private static readonly string GameServerBuildDirectory = "GameServer";
        private static readonly string LinuxGameServerBin = "multiplayer_tower_defense.x86_64";
        private static readonly string DevLinuxGameServerBuildDirectory = Path.Combine("Builds", GameServerBuildDirectory, "Dev");
        private static readonly string DebugLinuxGameServerBuildDirectory = Path.Combine("Builds", GameServerBuildDirectory, "Debug");
        private static readonly string ProdLinuxGameServerBuildDirectory = Path.Combine("Builds",GameServerBuildDirectory, "Prod");
        private static readonly string DebugOSXGameServerBuildDirectory = Path.Combine("Builds", GameServerBuildDirectory, "OSX_Debug");
        [MenuItem("Build/Build Linux Game Server (Dev)")]
        public static void BuildLinuxGameServerDev()
        {
            if (!Directory.Exists(DevLinuxGameServerBuildDirectory))
                Directory.CreateDirectory(DevLinuxGameServerBuildDirectory);
            // I have no idea but installing Mirror made Mono not work; IL2CPP build still works on my docker run --platform linux/amd64 commands
            PlayerSettings.SetScriptingBackend(BuildTargetGroup.Standalone, ScriptingImplementation.IL2CPP);
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
            Debug.Log("Server build succeeded and is located at: " + Path.Combine(GameServerBuildDirectory, LinuxGameServerBin));
        }
        [MenuItem("Build/Build Linux Game Server (Dev Debugging enabled)")]
        public static void BuildLinuxGameServerDevDebugging()
        {
            if (!Directory.Exists(DevLinuxGameServerBuildDirectory))
                Directory.CreateDirectory(DevLinuxGameServerBuildDirectory);
            // I have no idea but installing Mirror made Mono not work; IL2CPP build still works on my docker run --platform linux/amd64 commands
            PlayerSettings.SetScriptingBackend(BuildTargetGroup.Standalone, ScriptingImplementation.IL2CPP);
            var locationPathName = Path.Combine(DebugLinuxGameServerBuildDirectory, LinuxGameServerBin);
            var report = BuildPipeline.BuildPlayer(
                new BuildPlayerOptions
                {
                    scenes = GameScenes,
                    locationPathName = locationPathName,
                    target = BuildTarget.StandaloneLinux64,
                    subtarget = (int)StandaloneBuildSubtarget.Server,
                    options =  BuildOptions.Development | BuildOptions.AllowDebugging | BuildOptions.WaitForPlayerConnection
                }
            );
            if (report.summary.result != BuildResult.Succeeded)
            {
                Debug.LogError($"{report.summary.result} Server build failed with the following details: {report.summary}");
                return;
            }
            Debug.Log($"Server build succeeded and is located at: {locationPathName}");
        }
        [MenuItem("Build/Build Linux Game Server (Prod)")]
        public static void BuildLinuxGameServerProd()
        {
            if (!Directory.Exists(ProdLinuxGameServerBuildDirectory))
                Directory.CreateDirectory(ProdLinuxGameServerBuildDirectory);
            // I have no idea but installing Mirror made Mono not work; IL2CPP build still works on my docker run --platform linux/amd64 commands
            PlayerSettings.SetScriptingBackend(BuildTargetGroup.Standalone, ScriptingImplementation.IL2CPP);
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
            Debug.Log("Server build succeeded and is located at: " + Path.Combine(GameServerBuildDirectory, LinuxGameServerBin));
        }
        [MenuItem("Build/Build OSX Game Server (Dev Debugging Enabled)")]
        public static void BuildOSXGameServerDevDebugging()
        {
            if (!Directory.Exists(DevLinuxGameServerBuildDirectory))
                Directory.CreateDirectory(DevLinuxGameServerBuildDirectory);
            PlayerSettings.SetScriptingBackend(BuildTargetGroup.Standalone, ScriptingImplementation.Mono2x);
            var locationPathName = Path.Combine(DebugOSXGameServerBuildDirectory, LinuxGameServerBin);
            var report = BuildPipeline.BuildPlayer(
                new BuildPlayerOptions
                {
                    scenes = GameScenes,
                    locationPathName = locationPathName,
                    target = BuildTarget.StandaloneOSX,
                    subtarget = (int)StandaloneBuildSubtarget.Server,
                    options =  BuildOptions.Development | BuildOptions.AllowDebugging | BuildOptions.WaitForPlayerConnection
                }
            );
            if (report.summary.result != BuildResult.Succeeded)
            {
                Debug.LogError($"{report.summary.result} Server build failed with the following details: {report.summary}");
                return;
            }
            Debug.Log($"Server build succeeded and is located at: {locationPathName}");
        }
    }
}
