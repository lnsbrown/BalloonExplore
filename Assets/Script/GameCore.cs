using System;
using System.Collections.Generic;
using Script.Manager;
using UnityEngine;

namespace Script
{
    public class GameCore
    {
        private Dictionary<ManagerType, IManager> managerDic = new Dictionary<ManagerType, IManager>();

        // 场景管理器
        private SceneManager sceneManager;

        // UI管理器
        private UIManager uiManager;

        // 动画管理器
        private AnimManager animManager;

        public void Init()
        {
            Debug.Log("GameCore Init");

            this.AddManager(ManagerType.Animation);
            this.AddManager(ManagerType.Scene);
            this.AddManager(ManagerType.UI);

            foreach (var manager in this.managerDic.Values)
            {
                manager.Init();
            }
        }

        private void AddManager(ManagerType managerType)
        {
            var manager = ManagerFactory.CreateManager(managerType);
            managerDic.Add(managerType, manager);
        }

        public void Update()
        {
            foreach (var manager in this.managerDic.Values)
            {
                manager.Update();
            }
        }
    }
}