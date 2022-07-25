using System;
using Script.Logic.Manager;
using Script.Manager;
using Script.Object;
using UnityEngine;

namespace Script.Scene
{
    public class GameScene : ScriptableObject
    {
        // 背景
        private GameUnit bgUnit;

        // 玩家
        public GameUnit mainPlayer;

        // 单位管理器
        private UnitManager unitManager;

        public void Init()
        {
            unitManager = GameCore.GetInstance().GetManager<UnitManager>();
            // 创建背景
            bgUnit = CreateUnit<BgUnit>();
            // 创建玩家
            mainPlayer = CreateUnit<PlayerUnit>();

            // 初始化所有Unit
            unitManager.InitUnit();
        }

        private GameUnit CreateUnit<T>() where T : GameUnit
        {
            var unit = CreateInstance<T>();
            unitManager.AddUnit(unit);
            return unit;
        }

        public void Update()
        {
            unitManager.UpdateUnit();
        }
    }
}