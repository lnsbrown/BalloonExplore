using System;
using System.Collections.Generic;
using Script.Manager;
using UnityEngine;

namespace Script
{
    public class GameCore
    {
        private static GameCore INSTANCE = new GameCore();

        public static GameCore GetInstance()
        {
            return INSTANCE;
        }

        private GameCore()
        {
        }

        private Dictionary<ManagerType, IManager> managerDic = new Dictionary<ManagerType, IManager>();

        // 场景管理器
        private SceneManager sceneManager;

        // UI管理器
        private UIManager uiManager;

        // 动画管理器
        private AnimManager animManager;

        // 时间管理器
        private TimeManager timeManager;

        public void Init()
        {
            Application.targetFrameRate = 30;

            this.AddManager(ManagerType.Config);
            this.AddManager(ManagerType.Time);
            this.AddManager(ManagerType.Animation);
            this.AddManager(ManagerType.Scene);
            this.AddManager(ManagerType.UI);

            foreach (var manager in this.managerDic.Values)
            {
                manager.Init();
            }

            Debug.Log("GameCore Init");
            Debug.Log("Screen width:" + Screen.width);
            Debug.Log("Screen height:" + Screen.height);
            Debug.Log("TargetFrameRate:" + Application.targetFrameRate);
        }

        private void AddManager(ManagerType managerType)
        {
            var manager = ManagerFactory.CreateManager(managerType);
            if (manager == null)
            {
                return;
            }

            // 添加到manager管理
            managerDic.Add(managerType, manager);
        }

        public void Update()
        {
            foreach (var manager in this.managerDic.Values)
            {
                manager.Update();
            }
        }

        /// <summary>
        /// 获取manager
        /// </summary>
        /// <param name="managerType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetManager<T>(ManagerType managerType)
        {
            return (T)managerDic[managerType];
        }
    }
}