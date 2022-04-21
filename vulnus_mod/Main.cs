using System;
using HarmonyLib;
using UnityEngine;
using vulnus_mod.patches;

namespace vulnus_mod
{
    public class Main : MonoBehaviour
    {
        public void Start()
        {
            Utils.CreateConsole();
            
            Console.WriteLine("poggeres!!");
            
            Harmony.DEBUG = true;
            Patcher.Patch();
            
            
        }
    }
}