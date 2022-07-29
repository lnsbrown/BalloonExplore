using Script.Const;
using Script.Scene;
using UnityEditor;
using UnityEngine;

namespace Script.Object
{
    public class ObstacleUnit : GameUnit
    {
        protected override void InitGameObject()
        {
            gameObject = Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>(PrefabDefine.OBSTACLE));
        }

        protected override int GetSceneLayerIndex()
        {
            return Globals.configLayer.obstacle;
        }

        public override void Update()
        {
        }
    }
}