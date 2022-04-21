using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using vulnus_mod.patches;

namespace vulnus_mod
{
    public class Loader : MonoBehaviour
    {
        public static bool Debug = false;
        public static void Init()
        {
            GameObject _Load = new GameObject();
            _Load.AddComponent<Main>();
            GameObject.DontDestroyOnLoad(_Load);
        }

        public static void Init_D()
        {
            Debug = true;
            Init();
        }
        
        public static bool Unload()
        {
            _Unload();
            return true;
        }
        private static void _Unload()
        {
            Console.WriteLine("Unloaded, cyaaaa");
            GameObject.Destroy(_Load);
            Patcher.UnPatch();
            Console.WriteLine("killing console in 3 seconds.");
            Thread.Sleep(3000);
            Utils.DeallocConsole();
        }
        private GameObject _gameObject;
        static private GameObject _Load;
    }
}