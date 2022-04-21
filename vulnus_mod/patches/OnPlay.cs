using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using HarmonyLib;
using UnityEngine;

namespace vulnus_mod.patches
{
    [HarmonyPatch(typeof(SoundPlayer),"Load")]
    
    public class OnPlay
    {
        [HarmonyPrefix]
        static bool IncreaseSpeed(string file, bool autoplay, float volume, ref float pitch)
        {
            Console.WriteLine("Playing a map...");
            // pitch *=2;
            return true;
        }
    }
    [HarmonyPatch(typeof(MapPlayer),"Update")]
    public class OnMapPlayerUpdate
    {
        [HarmonyPostfix]
        static void OnUpdate(ref MapPlayer __instance)
        {
    
            var notesMap = (Dictionary<Map.Note, GameObject>) typeof(MapPlayer).GetField("Notes", BindingFlags.NonPublic | BindingFlags.Instance)
                .GetValue(__instance);
            var AT = (float) typeof(MapPlayer).GetField("AT", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(__instance);
            var AD = (float) typeof(MapPlayer).GetField("AD", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(__instance);
            var colors = (Color[]) typeof(MapPlayer).GetField("colors", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(__instance);
            Console.WriteLine($"poggers; {AT} & {AD}");
            foreach (var notes in notesMap)
            {
                try
                {
                    var offset = notes.Key.Time - MapPlayer.RealTime;
                    var offset2 = AD * (offset / AT);
                    // var meshRenderMaterial = notes.Value.GetComponent<MeshRenderer>().material;
                    // var color = meshRenderMaterial.color;
                    // color.a = offset2;
                    var c = notes.Value.transform.Find("Mesh").GetComponent<MeshRenderer>().material.color;
                    var calc = Math.Max((offset2) - 5,0);
                    if (calc > 60) continue;
                    c.a = Math.Min((calc / 10),1);
                    notes.Value.transform.Find("Mesh").GetComponent<MeshRenderer>().material.color = c;
                    // Console.WriteLine($"offset2 | {c.a}");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"oopsies!! {e}");
                }
            }
        }
    }
}