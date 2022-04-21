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
            
            Console.WriteLine("poggeres!! the mod has been loaded!!!");
            
            Harmony.DEBUG = true;
            Patcher.Patch();
            
            
        }

        private bool ignoreKey = false;

        private void OnGUI()
        {
            //scuffed.
            if (Event.current.keyCode == KeyCode.Z && Event.current.control )
            {
                if (ignoreKey) return;
                ignoreKey = true;
                Settings.MainGuiOpen = !Settings.MainGuiOpen;
            }
            else
            {
                ignoreKey = false;
            }
            if (Settings.MainGuiOpen)
                GUI.Window(0, new Rect(20, 20, 220, 500), mainWindow, "Main window");
        }

        void mainWindow(int winid)
        {
            GUI.DragWindow(new Rect(0, 0, 0x1000, 30));
            int yPos = 20;

            
            var fontStyle = new GUIStyle();

            var textWidthGhost = fontStyle.CalcSize(new GUIContent("Toggle ghost mode")).x;
            var textWidthUnload = fontStyle.CalcSize(new GUIContent("Unload mod")).x;
            Settings.EnableGhost =
                GUI.Toggle(new Rect(10, yPos += 20, 20+textWidthGhost, 20), Settings.EnableGhost, "Toggle ghost mode");
            // if (GUI.Button(new Rect(10, yPos += 20, 20 + textWidthUnload, 20), "Unload mod"))
            // {
            //     Loader.Unload();
            // }
            GUI.TextField(new Rect(10, yPos += 20, 200, 20),"press ctrl+z to close / open.");
        }
    }
}