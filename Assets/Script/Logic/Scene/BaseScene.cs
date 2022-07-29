using System;
using Script.Enums;
using Script.Logic.Manager;
using Script.Object;
using UnityEngine;

namespace Script.Scene
{
    public abstract class BaseScene : ScriptableObject
    {
        // 单位管理器
        private readonly UnitManager unitManager = GameCore.GetInstance().GetManager<UnitManager>();

        /// <summary>
        /// 初始化
        /// </summary>
        public abstract void Init();

        /// <summary>
        /// 在场景中创建单位
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T CreateUnit<T>() where T : GameUnit
        {
            var unit = CreateInstance<T>();
            unit.scene = this;
            unitManager.AddUnit(unit);
            return unit;
        }

        /// <summary>
        /// 获取场景中的单位
        /// </summary>
        /// <param name="unitId"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetUnit<T>(int unitId) where T : GameUnit
        {
            return unitManager.GetUnit(unitId) as T;
        }

        public void Update()
        {
            unitManager.UpdateUnit();
        }
    }
}