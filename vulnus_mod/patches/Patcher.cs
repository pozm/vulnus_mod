using System;
using HarmonyLib;

namespace vulnus_mod.patches
{
    public class Patcher
    {
        public static Harmony harm;
        public static void Patch()
        {
                
            harm = new Harmony("luna.cheat.vulnus");
            Console.WriteLine("Created harmony");

                
            harm.PatchAll(typeof(OnPlay).Assembly);
                
        }

        public static void UnPatch()
        {
            Console.WriteLine("Unpatched.");
            harm.UnpatchAll();
        }
    }
}