using Script.Logic.Manager;
using Script.Manager;
using Script.Scene;
using UnityEngine;

namespace Script
{
    public class SceneManager : BaseManager
    {
        public GameScene gameScene;

        public SceneManager(ManagerType managerType) : base(managerType)
        {
        }

        public override void Init()
        {
            Debug.Log("SceneManager Init");
            gameScene = ScriptableObject.CreateInstance<GameScene>();
            gameScene.Init();
        }

        public override void Update()
        {
            gameScene.Update();
        }

        /// <summary>
        /// 切换场景
        /// </summary>
        public void OnSwitchScene()
        {
            ClearUnits();
        }

        private static void ClearUnits()
        {
            var unitManager = GameCore.GetInstance().GetManager<UnitManager>();
            unitManager?.ClearAllUnit();
        }
    }
}