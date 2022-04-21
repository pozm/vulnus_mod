using System;
using BepInEx;
using HarmonyLib;
using UnityEngine;
using vulnusPlugin.Patches;

namespace vulnusPlugin
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        private void Awake()
        {
            // Plugin startup logic
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
            var harmony = Harmony.CreateAndPatchAll(typeof(Patches.Startup));
            harmony.PatchAll(typeof(GhostMod));
        }
        private void Start()
        {

            
        }
    }
}
