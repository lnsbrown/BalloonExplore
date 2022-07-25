using System;
using Script.Config;
using Script.Const;
using Script.Enums;
using Script.Manager;
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
        private float curHorSpeed;

        // 操作向左
        public bool operateLeft;

        // 操作向右
        public bool operateRight;

        protected override void InitGameObject()
        {
            gameObject = Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>(PrefabDefine.PLAYER));
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
            gameObject.transform.localPosition = transformLocalPosition;
        }

        public override void Update()
        {
            HorMove();
        }

        private void updateHorAccelerate()
        {
            
            // TODO 明天再搞
        }

        /// <summary>
        /// 水平移动
        /// </summary>
        private void HorMove()
        {
            // 更新当前速度
            curHorSpeed += horAccelerate * Time.deltaTime;

            // 计算移动举例
            var distance = curHorSpeed / 2 * Time.deltaTime;
            if (distance == 0)
            {
                return;
            }

            // 调整位置
            var transformLocalPosition = gameObject.transform.localPosition;
            transformLocalPosition.x += distance;
            // 判断左边界
            transformLocalPosition.x =
                Mathf.Max(transformLocalPosition.x, -Screen.width / 2 + configPlayer.balloonWidth / 2);
            // 判断右边界
            transformLocalPosition.x =
                Mathf.Min(transformLocalPosition.x, Screen.width / 2 - configPlayer.balloonWidth / 2);

            gameObject.transform.localPosition = transformLocalPosition;
        }

        /// <summary>
        /// 更新水平加速度
        /// </summary>
        public void UpdateHorizonAcceleration(float horAccelerate)
        {
            this.horAccelerate = horAccelerate;
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