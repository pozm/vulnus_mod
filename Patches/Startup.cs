using System;
using System.Collections;
using System.Collections.Generic;
using HarmonyLib;
using BepInEx;
using UnityEngine;
using Logger = BepInEx.Logging.Logger;

namespace vulnusPlugin.Patches;

public class Startup
{
    [HarmonyPatch(typeof(MenuController), "Start")]
    [HarmonyPrefix]
    static void OnSetup(MenuController __instance)
    {
        // var ghostObj = GameObject.Find("Speed");
        __instance.ModsMenu.transform.Find("Viewport").Find("Content").Find("Ghost").gameObject.SetActive(true);
        Console.WriteLine("pogg");

    }
}