using System;
using Script.Config;
using Script.Const;
using Script.Enums;
using Script.Manager;
using Script.Scene;
using UnityEditor;
using UnityEngine;

namespace Script.Object
{
    public class PlayerUnit : GameUnit
    {

        private BalloonComponent balloonView;

        public ConfigPlayer configPlayer;

        // 水平加速度
        private float horAccelerate;

        // 当前水平速度
        private float curHorizontalSpeed;

        // 当前垂直速度
        private float curVerticalSpeed;

        // 操作向左
        public bool operateLeft;

        // 操作向右
        public bool operateRight;

        // 状态
        private StateEnum stateEnum;

        // 状态
        public enum StateEnum
        {
            // 停止中
            STOPPED,

            // 悬浮中
            FLOATING,

            // 悬停中
            FLOAT_STOPPED,
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

            this.stateEnum = stateEnum;

            // Debug.Log("Enter State:" + stateEnum);
        }


        protected override void InitGameObject()
        {
            gameObject = Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>(PrefabDefine.PLAYER));
        }

        protected override int GetSceneLayerIndex()
        {
            return Globals.configLayer.player;
        }

        private GameObject balloon;

        public override void Init()
        {
            base.Init();
            // 玩家配置
            configPlayer = GameCore.GetInstance().GetManager<ConfigManager>().configPlayer;
            // 气球的显示层
            balloonView = gameObject.GetComponentInChildren<BalloonComponent>();

            var renderer = gameObject.GetComponentInChildren<Renderer>();

            var balloonWidth = configPlayer.balloonWidth;
            var scale = balloonWidth / renderer.bounds.size.x * 1f;
            var balloonHeight = renderer.bounds.size.x * 1f * scale;
            // 调整缩放适配屏幕
            var transformLocalScale = gameObject.transform.localScale;
            transformLocalScale.x = scale;
            transformLocalScale.y = scale;
            gameObject.transform.localScale = transformLocalScale;
            // 调整位置
            var transformLocalPosition = gameObject.transform.localPosition;
            transformLocalPosition.x = 0;
            transformLocalPosition.y = -Screen.height / 2 + configPlayer.balloonHoverHeight + balloonHeight / 2;
            transformLocalPosition.z = unitSceneLayerIndex;
            gameObject.transform.localPosition = transformLocalPosition;

            EnterState(StateEnum.STOPPED);
        }

        public override void Update()
        {
            UpdateHorAccelerate();
            HorizontalMove();
            VerticalMove();
        }

        private void UpdateHorAccelerate()
        {
            if ((operateLeft && operateRight) || (!operateLeft && !operateRight))
            {
                // 左右都按下或都没按下
                if (curHorizontalSpeed == 0)
                {
                    // 当前没速度，就保持静止不动
                    horAccelerate = 0;
                    return;
                }

                // 按移动方向反向加速
                horAccelerate = curHorizontalSpeed > 0 ? -configPlayer.blockAccelerate : configPlayer.blockAccelerate;
            }
            else if (operateLeft)
            {
                // 只按下左
                horAccelerate = configPlayer.operateLeftAccelerate;
            }
            else if (operateRight)
            {
                // 只按下右
                horAccelerate = configPlayer.operateRightAccelerate;
            }
        }

        public void StartMove()
        {
            // 初始悬浮速度
            curVerticalSpeed = configPlayer.initFloatSpeed;
            EnterState(StateEnum.FLOATING);
        }

        /**
         * 垂直移动
         */
        private void VerticalMove()
        {
            if (stateEnum != StateEnum.FLOATING)
            {
                return;
            }

            curVerticalSpeed += Time.deltaTime * this.configPlayer.blockFloatAccelerate;
            if (curVerticalSpeed <= 0)
            {
                EnterState(StateEnum.FLOAT_STOPPED);
                return;
            }

            // 计算移动距离
            var distance = curVerticalSpeed * Time.deltaTime;
            if (distance == 0)
            {
                return;
            }

            // 调整位置
            var transformLocalPosition = gameObject.transform.localPosition;
            transformLocalPosition.y += distance;
            gameObject.transform.localPosition = transformLocalPosition;
        }

        /// <summary>
        /// 水平移动
        /// </summary>
        private void HorizontalMove()
        {
            // 更新当前速度
            curHorizontalSpeed += horAccelerate * Time.deltaTime;

            // 计算移动距离
            var distance = curHorizontalSpeed * Time.deltaTime;
            if (distance == 0)
            {
                return;
            }

            // 调整位置
            var transformLocalPosition = gameObject.transform.localPosition;
            transformLocalPosition.x += distance;
            // 判断左边界
            if (transformLocalPosition.x <= -Screen.width / 2 + configPlayer.balloonWidth / 2)
            {
                // 走到头，速度归0
                curHorizontalSpeed = 0;
                transformLocalPosition.x = -Screen.width / 2 + configPlayer.balloonWidth / 2;
            }

            // 判断右边界
            if (transformLocalPosition.x >= Screen.width / 2 - configPlayer.balloonWidth / 2)
            {
                // 走到头，速度归0
                curHorizontalSpeed = 0;
                transformLocalPosition.x = Screen.width / 2 - configPlayer.balloonWidth / 2;
            }

            gameObject.transform.localPosition = transformLocalPosition;
        }

        /// <summary>
        /// 更换气球皮肤
        /// </summary>
        /// <param name="skinType"></param>
        public void UpdateBalloonSkin(BalloonSkinType skinType)
        {
            var index = Convert.ToInt32(skinType);
            if (index >= balloonView.skins.Length)
            {
                Debug.LogError($"UpdateBalloonSkin err skinType {skinType} no config");
                return;
            }

            var skin = balloonView.skins[index];
            balloonView.balloonRender.sprite = skin;
        }
    }
}