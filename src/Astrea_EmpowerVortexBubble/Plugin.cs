using BepInEx;
using BepInEx.Logging;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using System.Reflection;

namespace Astrea_EmpowerVortexBubble
{
    [BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
    [BepInProcess(Constants.GAME_PROCESS_NAME)]
    public class Plugin : BasePlugin
    {
        internal static ManualLogSource PluginLogger;

        public Plugin()
        {
            // Keep the default plugin log source around so we don't have to create ManualLogSources everywhere.
            PluginLogger = Log;
        }

        public override void Load()
        {
            var harmony = new Harmony(MyPluginInfo.PLUGIN_GUID);

            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }
}