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
        }

        public override void Update()
        {
            if (Time.frameCount % 5 == 0)
            {
                return;
            }

            RollDown();
        }

        private void RollDown()
        {
            bg.transform.localPosition =
                Vector3.MoveTowards(bg.transform.localPosition,
                    new Vector3(bg.transform.localPosition.x, bg.transform.localPosition.y - 0.01F,
                        bg.transform.localPosition.z), Time.deltaTime);
        }
    }
}