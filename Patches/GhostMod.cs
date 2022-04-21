using System;
using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using UnityEngine;

namespace vulnusPlugin.Patches;

public class GhostMod
{
    [HarmonyPatch(typeof(MapPlayer),"Update")]
    [HarmonyPostfix]
    static void onMapPlayerUpdate(ref MapPlayer __instance)
    {
        var notesMap = (Dictionary<Map.Note, GameObject>) typeof(MapPlayer).GetField("Notes", BindingFlags.NonPublic | BindingFlags.Instance)
            .GetValue(__instance);
        var AT = (float) typeof(MapPlayer).GetField("AT", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(__instance);
        var AD = (float) typeof(MapPlayer).GetField("AD", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(__instance);

            
        if (!MapPlayer.Mods.Ghost) return;

        foreach (var notes in notesMap)
        {
            var offset = notes.Key.Time - MapPlayer.RealTime;
            var offset2 = AD * (offset / AT);

            var c = notes.Value.transform.Find("Mesh").GetComponent<MeshRenderer>().material.color;
            var calc = Math.Max((offset2) - AD / 4, 0);
            if (calc > 50) continue;
            c.a = Math.Min((calc / 10), 1);
            notes.Value.transform.Find("Mesh").GetComponent<MeshRenderer>().material.color = c;
        }
    }
}