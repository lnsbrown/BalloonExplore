using System;
using Script.Config;
using Script.Exception;
using UnityEngine;

namespace Script.Manager
{
    public class ConfigManager : IManager
    {
        public ConfigBgMap configBgMap { get; private set; }
        public ConfigGlobal configGlobal { get; private set; }

        public void Init()
        {
            Debug.Log("ConfigManager Init");
            // 加载所有配置
            configBgMap = JsonConfig.Load<ConfigBgMap>("BgMap");
            configGlobal = JsonConfig.Load<ConfigGlobal>("Global");
        }

        public void Update()
        {
        }
    }
}