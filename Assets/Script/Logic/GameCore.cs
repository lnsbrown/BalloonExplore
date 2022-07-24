using System;
using System.Collections.Generic;
using Script.Manager;
using UnityEngine;

namespace Script
{
    public class GameCore
    {
        private static readonly GameCore INSTANCE = new GameCore();

        public static GameCore GetInstance()
        {
            return INSTANCE;
        }

        private GameCore()
        {
        }

        private readonly Dictionary<ManagerType, IManager> managerDic = new Dictionary<ManagerType, IManager>();
        private readonly Dictionary<Type, ManagerType> managerTypeDic = new Dictionary<Type, ManagerType>();

        public void Init()
        {
            Application.targetFrameRate = 30;

            AddManager(ManagerType.Config);
            AddManager(ManagerType.Time);
            AddManager(ManagerType.Animation);
            AddManager(ManagerType.Scene);
            AddManager(ManagerType.UI);
            AddManager(ManagerType.Unit);

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
            managerTypeDic.Add(manager.GetType(), managerType);
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
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetManager<T>() where T : class, IManager
        {
            var type = typeof(T);
            if (!managerTypeDic.ContainsKey(type))
            {
                return null;
            }

            var managerType = managerTypeDic[type];
            if (!managerDic.ContainsKey(managerType))
            {
                return null;
            }

            return managerDic[managerType] as T;
        }

        /// <summary>
        /// 获取manager
        /// </summary>
        /// <param name="managerType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetManager<T>(ManagerType managerType) where T : class, IManager
        {
            if (!managerDic.ContainsKey(managerType))
            {
                return null;
            }

            return managerDic[managerType] as T;
        }
    }
}