using System.Collections.Generic;
using Script.Enums;
using Script.Manager;
using Script.Object;
using Script.Util;
using UnityEngine;

namespace Script.Logic.Manager
{
    public class UnitManager : BaseManager
    {
        private readonly Dictionary<int, GameUnit> unitDic = new Dictionary<int, GameUnit>();

        private List<GameUnit> removeList = new List<GameUnit>();

        public UnitManager(ManagerType managerType) : base(managerType)
        {
        }

        public override void Init()
        {
            Debug.Log("UIManager Init");
        }

        public void UpdateUnit()
        {
            foreach (var unit in unitDic.Values)
            {
                if (unit.needRemove)
                {
                    removeList.Add(unit);
                }

                unit.Update();
            }

            // 移除列表
            if (removeList.Count > 0)
            {
                foreach (var gameUnit in removeList)
                {
                    gameUnit.OnRemove();
                    unitDic.Remove(gameUnit.unitId);
                }

                removeList.Clear();
            }
        }

        public override void Update()
        {
        }

        /// <summary>
        /// 添加单位
        /// </summary>
        /// <param name="gameUnit"></param>
        public void AddUnit(GameUnit gameUnit)
        {
            if (gameUnit == null)
            {
                return;
            }

            if (gameUnit.unitId > 0 && unitDic.ContainsKey(gameUnit.unitId))
            {
                return;
            }

            var unitId = IdCreator.GetInstance().Incr(IdIncreaseType.UNIT);
            gameUnit.unitId = unitId;
            unitDic.Add(unitId, gameUnit);
        }

        /// <summary>
        /// 获取Unit
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public GameUnit GetUnit(int unitId)
        {
            return unitDic[unitId];
        }

        public void ClearAllUnit()
        {
            unitDic.Clear();
        }
    }
}