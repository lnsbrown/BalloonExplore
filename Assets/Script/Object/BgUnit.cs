using System;
using UnityEditor;
using UnityEngine;

namespace Script.Object
{
    public class BgUnit : GameUnit
    {
        private GameObject bg;

        public override void Init()
        {
            bg = Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefab/bgMap.prefab"));
            Component[] maps = bg.GetComponentsInChildren<Component>();
            Debug.Log(maps);
        }

        public override void Update()
        {
        }
    }
}