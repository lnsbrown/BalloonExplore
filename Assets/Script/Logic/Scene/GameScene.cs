using Script.Enums;
using Script.Logic.Manager;
using Script.Object;
using UnityEngine;

namespace Script.Scene
{
    public class GameScene : ScriptableObject
    {
        private GameState gameState;

        // 背景
        private BgUnit bgUnit;

        // 玩家
        public PlayerUnit mainPlayer;

        // 单位管理器
        private UnitManager unitManager;

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

        public void Init()
        {
            unitManager = GameCore.GetInstance().GetManager<UnitManager>();
            // 创建背景
            bgUnit = CreateUnit<BgUnit>();
            // 创建玩家
            mainPlayer = CreateUnit<PlayerUnit>();

            // 初始化所有Unit
            unitManager.InitUnit();

            EnterState(GameState.Init, true);
        }

        private T CreateUnit<T>() where T : GameUnit
        {
            var unit = CreateInstance<T>();
            unitManager.AddUnit(unit);
            return unit;
        }

        public void Update()
        {
            unitManager.UpdateUnit();
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