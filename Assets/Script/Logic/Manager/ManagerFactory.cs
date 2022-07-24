using System;
using Script.Logic.Manager;

namespace Script.Manager
{
    public class ManagerFactory
    {
        /**
         * 创建Manager
         */
        public static IManager CreateManager(ManagerType managerType)
        {
            switch (managerType)
            {
                case ManagerType.Scene:
                    return new SceneManager(managerType);
                case ManagerType.UI:
                    return new UIManager(managerType);
                case ManagerType.Animation:
                    return new AnimManager(managerType);
                case ManagerType.Time:
                    return new TimeManager(managerType);
                case ManagerType.Config:
                    return new ConfigManager(managerType);
                case ManagerType.Unit:
                    return new UnitManager(managerType);
                default:
                    return null;
            }
        }
    }
}