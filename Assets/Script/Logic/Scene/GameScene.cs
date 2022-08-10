using System;
using Script.Config;
using Script.Enums;
using Script.Manager;
using Script.Object;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using Random = System.Random;

namespace Script.Scene
{
    public class GameScene : BaseScene
    {
        private GameState gameState;

        // 背景
        private BgUnit bgUnit;

        // 玩家
        public PlayerUnit mainPlayer;

        // 障碍物配置
        private ConfigObstacle configObstacle;

        private Random random;

        // ReSharper disable Unity.PerformanceAnalysis
        /// <summary>
        /// 进入状态
        /// </summary>
        /// <param name="stateEnum"></param>
        public bool EnterState(GameState gameState, bool forceEnter = false)
        {
            if (this.gameState == gameState)
            {
                return false;
            }

            if (!forceEnter && !GameStateTool.CanEnter(this.gameState, gameState))
            {
                Debug.LogError($"state[{this.gameState}] enter game state[{gameState}] failed");
                return false;
            }

            this.gameState = gameState;
            Debug.Log($"state[{this.gameState}] enter game state[{gameState}]");
            return true;
        }

        public override void Init()
        {
            configObstacle = GameCore.GetInstance().GetManager<ConfigManager>().configObstacle;
            random = new Random();

            // 创建背景
            bgUnit = CreateUnit<BgUnit>();
            bgUnit.Init();
            // 创建玩家
            mainPlayer = CreateUnit<PlayerUnit>();
            mainPlayer.Init();
            // 进入初始状态
            EnterState(GameState.Init, true);
        }

        public void Start()
        {
            // 进入游戏状态
            if (!EnterState(GameState.Gaming))
            {
                return;
            }

            mainPlayer.StartMove();
            bgUnit.EnterState(BgUnit.StateEnum.STARTING);
        }

        private long nextCreateObstacleTime = 0;

        protected override void OnUpdate()
        {
            base.OnUpdate();

            var now = Globals.Now();
            if (now >= nextCreateObstacleTime)
            {
                nextCreateObstacleTime = now + configObstacle.createInterval;
                // 随机创建
                var randomSpaceLength = random.Next(50, 600);
                var randomSpaceOffset = random.Next(0, 600);
                CreateObstacles(randomSpaceLength, randomSpaceOffset, bgUnit.GetMovingSpeed());
            }
        }

        /// <summary>
        /// 创建障碍物
        /// </summary>
        /// <param name="spaceLength">空隙长度</param>
        /// <param name="spaceOffset">空隙偏移（以最左边为标准）</param>
        private void CreateObstacles(float spaceLength, float spaceOffset, float movingSpeed)
        {
            if (spaceLength >= Screen.width)
            {
                // 空隙长度超过屏幕了，不用创建障碍物
                return;
            }

            // 空隙长度边界
            spaceLength = Math.Max(spaceLength, configObstacle.obstacleMinSpace);
            spaceLength = Math.Min(spaceLength, Screen.width);
            // 偏移边界
            spaceOffset = Math.Max(spaceOffset, 0);
            spaceOffset = Math.Min(spaceOffset, Screen.width - spaceLength);

            var offsetX = -Screen.width / 2 + spaceOffset;

            if (offsetX <= -Screen.width / 2)
            {
                // 靠右障碍物
                var posX = offsetX + spaceLength;
                CreateOneObstacle(ObstacleUnit.Type.LEFT, posX, movingSpeed);
                posX += configObstacle.obstacleWidth;

                while (posX < Screen.width / 2 + configObstacle.obstacleWidth)
                {
                    CreateOneObstacle(ObstacleUnit.Type.CENTER, posX, movingSpeed);
                    posX += configObstacle.obstacleWidth;
                }
            }
            else if (offsetX > -Screen.width / 2 && (offsetX + spaceLength) >= Screen.width / 2)
            {
                // 靠左障碍物
                var posX = offsetX;
                CreateOneObstacle(ObstacleUnit.Type.RIGHT, posX, movingSpeed);
                posX -= configObstacle.obstacleWidth;

                while (posX > -Screen.width / 2 - configObstacle.obstacleWidth)
                {
                    CreateOneObstacle(ObstacleUnit.Type.CENTER, posX, movingSpeed);
                    posX -= configObstacle.obstacleWidth;
                }
            }
            else
            {
                // 左右都有障碍物
                // 创建左边障碍物
                var posX = offsetX;
                CreateOneObstacle(ObstacleUnit.Type.RIGHT, posX, movingSpeed);
                posX -= configObstacle.obstacleWidth;

                while (posX > -Screen.width / 2 - configObstacle.obstacleWidth)
                {
                    CreateOneObstacle(ObstacleUnit.Type.CENTER, posX, movingSpeed);
                    posX -= configObstacle.obstacleWidth;
                }

                // 创建右边障碍物
                posX = offsetX + spaceLength;
                CreateOneObstacle(ObstacleUnit.Type.LEFT, posX, movingSpeed);
                posX += configObstacle.obstacleWidth;

                while (posX < Screen.width / 2 + configObstacle.obstacleWidth)
                {
                    CreateOneObstacle(ObstacleUnit.Type.CENTER, posX, movingSpeed);
                    posX += configObstacle.obstacleWidth;
                }
            }
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private void CreateOneObstacle(ObstacleUnit.Type type, float x, float movingSpeed)
        {
            var obstacleUnit = CreateUnit<ObstacleUnit>();
            obstacleUnit.type = type;
            obstacleUnit.movingSpeed = movingSpeed;
            obstacleUnit.Init();
            // 调整位置
            var transformPosition = obstacleUnit.gameObject.transform.position;
            transformPosition.x = x + configObstacle.obstacleWidth / 2;
            transformPosition.y =
                Screen.height / 2 + configObstacle.obstacleHeight / 2;

            // 大小缩放
            var transformLocalScale = obstacleUnit.gameObject.transform.localScale;
            transformLocalScale.x = configObstacle.obstacleWidth /
                                    obstacleUnit.gameObject.GetComponent<Renderer>().bounds.size.x;
            transformLocalScale.y = configObstacle.obstacleHeight /
                                    obstacleUnit.gameObject.GetComponent<Renderer>().bounds.size.y;

            obstacleUnit.gameObject.transform.position = transformPosition;
        }
    }
}