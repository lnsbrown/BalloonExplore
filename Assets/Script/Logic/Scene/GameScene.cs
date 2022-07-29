using Script.Enums;
using Script.Object;
using UnityEngine;

namespace Script.Scene
{
    public class GameScene : BaseScene
    {
        private GameState gameState;

        // 背景
        private BgUnit bgUnit;

        // 玩家
        public PlayerUnit mainPlayer;

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
    }
}