using System;
using Script.Config;
using Script.Const;
using Script.Manager;
using UnityEditor;
using UnityEngine;

namespace Script.Object
{
    public class BgUnit : GameUnit
    {
        // 背景配置
        private ConfigBgMap configBgMap;

        // 移动状态
        private volatile StateEnum stateEnum = StateEnum.STOPPED;

        // 进入状态时间
        private long enterStateTime;

        // 观测移动中的地图
        private GameObject watchMapObject;
        private int watchMapObjectIndex;

        // 地图
        private GameObject[] mapObjects;

        // 状态
        public enum StateEnum
        {
            // 启动中
            STARTING,

            // 移动中
            MOVING,

            // 停止中
            STOPPING,

            // 已停止
            STOPPED
        }

        protected override void InitGameObject()
        {
            gameObject = Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>(PrefabDefine.BG_MAP));
        }

        public override void Init()
        {
            base.Init();

            configBgMap = GameCore.GetInstance().GetManager<ConfigManager>().configBgMap;
            mapObjects = GameObject.FindGameObjectsWithTag("map");

            for (var index = 0; index < mapObjects.Length; index++)
            {
                var mapObject = mapObjects[index];
                if (index == 0)
                {
                    // 观察第一个地图的移动情况
                    watchMapObject = mapObject;
                    watchMapObjectIndex = index;
                }

                var renderer = mapObject.GetComponentInChildren<Renderer>();

                var scaleX = Screen.width / 2f / renderer.bounds.size.x * 1f;
                var scaleY = Screen.height / 2f / renderer.bounds.size.y * 1f;
                // 调整缩放适配屏幕
                var transformLocalScale = mapObject.transform.localScale;
                transformLocalScale.x = scaleX;
                transformLocalScale.y = scaleY;
                mapObject.transform.localScale = transformLocalScale;
                // 调整位置
                var transformLocalPosition = mapObject.transform.localPosition;
                transformLocalPosition.x = 0;
                transformLocalPosition.y = Screen.height * index;
                mapObject.transform.localPosition = transformLocalPosition;
            }

            // TODO 后期改为通过输入切换状态
            EnterState(StateEnum.STARTING);
        }

        /// <summary>
        /// 进入状态
        /// </summary>
        /// <param name="stateEnum"></param>
        public void EnterState(StateEnum stateEnum)
        {
            if (this.stateEnum == stateEnum)
            {
                return;
            }

            enterStateTime = Globals.Now();
            this.stateEnum = stateEnum;

            // Debug.Log("Enter State:" + stateEnum);
        }

        public override void Update()
        {
            RollDown();
        }

        private void RollDown()
        {
            // 计算移动举例
            var moveDistance = GetMoveDistance();
            if (moveDistance == 0)
            {
                return;
            }

            foreach (var mapObject in mapObjects)
            {
                // 移动所有地图
                MoveMap(moveDistance, mapObject);
            }

            // 是否移动到边界
            if (IsMapOverDown())
            {
                // 交换地图位置
                SwapMap();
            }
        }

        // 根据不同状态获取移动举例
        private float GetMoveDistance()
        {
            if (stateEnum == StateEnum.STOPPED)
            {
                return 0;
            }

            if (stateEnum == StateEnum.MOVING)
            {
                return Time.deltaTime * this.configBgMap.moveSpeed;
            }

            var now = Globals.Now();
            // 计算当前速度
            var v0 = stateEnum == StateEnum.STARTING ? 0 : configBgMap.moveSpeed;
            var a = stateEnum == StateEnum.STARTING ? configBgMap.startAccSpeed : configBgMap.stopAccSpeed;
            var t = (now - enterStateTime) / 1000f;
            var v = v0 + a * t;

            switch (stateEnum)
            {
                case StateEnum.STARTING when v >= configBgMap.moveSpeed:
                    // 速度加速到移动速度，进入移动状态
                    EnterState(StateEnum.MOVING);
                    return Time.deltaTime * configBgMap.moveSpeed;
                case StateEnum.STOPPING when v <= 0:
                    // 速度衰减到0，进入停止状态
                    EnterState(StateEnum.STOPPED);
                    return 0;
                default:
                    return Time.deltaTime * v;
            }
        }

        // 地图是否超出下边界
        private bool IsMapOverDown()
        {
            // y坐标小于等于屏幕，则认为超出边界
            return watchMapObject.transform.localPosition.y <= -Screen.height;
        }

        private void SwapMap()
        {
            int allLen = this.mapObjects.Length;

            // 移动地图
            var transformLocalPosition = watchMapObject.transform.localPosition;
            transformLocalPosition.y = Screen.height * (allLen - 1);
            watchMapObject.transform.localPosition = transformLocalPosition;

            // 下一个观察的地图对象
            int nextIndex = watchMapObjectIndex + 1;
            if (nextIndex >= allLen)
            {
                nextIndex = 0;
            }

            // 转移到下一个观察目标
            watchMapObject = mapObjects[nextIndex];
            watchMapObjectIndex = nextIndex;
        }

        private void MoveMap(float moveDistance, GameObject mapObject)
        {
            var localPosition = mapObject.transform.localPosition;
            localPosition.y -= moveDistance;
            mapObject.transform.localPosition = localPosition;
        }
    }
}