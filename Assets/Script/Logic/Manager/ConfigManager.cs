using System;
using Script.Config;
using Script.Exception;
using Script.Logic.Manager;
using UnityEngine;

namespace Script.Manager
{
    public class ConfigManager : BaseManager
    {
        public ConfigBgMap configBgMap { get; private set; }
        public ConfigGlobal configGlobal { get; private set; }
        public ConfigPlayer configPlayer { get; private set; }
        public ConfigObstacle configObstacle { get; private set; }
        public ConfigLayer configLayer { get; private set; }

        public ConfigManager(ManagerType managerType) : base(managerType)
        {
        }

        public override void Init()
        {
            Debug.Log("ConfigManager Init");
            // 加载所有配置
            configBgMap = JsonConfig.Load<ConfigBgMap>("BgMap");
            configGlobal = JsonConfig.Load<ConfigGlobal>("Global");
            configPlayer = JsonConfig.Load<ConfigPlayer>("Player");
            configLayer = JsonConfig.Load<ConfigLayer>("Layer");
            configObstacle = JsonConfig.Load<ConfigObstacle>("Obstacle");
        }

        public override void Update()
        {
        }
    }
}