using System;
using HarmonyLib;

namespace vulnus_mod.patches
{
    public class Patcher
    {
        public static Harmony harm;
        public static void Patch()
        {
                
            harm = new Harmony("luna.mod.vulnus");
            Console.WriteLine("Applied Harmony patches [luna.mod.vulnus].");

                
            harm.PatchAll(typeof(OnPlay).Assembly);
                
        }

        public static void UnPatch()
        {
            Console.WriteLine("Unpatched.");
            harm.UnpatchAll();
        }
    }
}