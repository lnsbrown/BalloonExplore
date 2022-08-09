using System;
using Script.Config;
using Script.Const;
using Script.Manager;
using UnityEditor;
using UnityEngine;

namespace Script.Object
{
    public class ObstacleUnit : GameUnit
    {
        public enum Type
        {
            LEFT,
            RIGHT,
            CENTER
        }

        public Type type = Type.CENTER;

        // 移动速度
        public float movingSpeed;

        private ConfigObstacle configObstacle;

        public override void Init()
        {
            base.Init();
            configObstacle = GameCore.GetInstance().GetManager<ConfigManager>().configObstacle;
        }

        protected override void InitGameObject()
        {
            gameObject = type switch
            {
                Type.LEFT => Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>(PrefabDefine.OBSTACLE_LEFT)),
                Type.RIGHT => Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>(PrefabDefine.OBSTACLE_RIGHT)),
                Type.CENTER => Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>(PrefabDefine.OBSTACLE_CENTER)),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        protected override int GetSceneLayerIndex()
        {
            return Globals.configLayer.obstacle;
        }

        private void Move()
        {
            var transformPosition = gameObject.transform.position;
            transformPosition.y -= Time.deltaTime * movingSpeed;
            gameObject.transform.position = transformPosition;
        }

        public override void Update()
        {
            Move();
            if (gameObject.transform.position.y < -Screen.height / 2 - configObstacle.obstacleHeight / 2)
            {
                // 移动出屏幕了,移除
                needRemove = true;
            }
        }
    }
}