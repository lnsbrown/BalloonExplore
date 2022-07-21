using System;

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
                    return new SceneManager();
                case ManagerType.UI:
                    return new UIManager();
                case ManagerType.Animation:
                    return new AnimManager();
                case ManagerType.Time:
                    return new TimeManager();
                case ManagerType.Config:
                    return new ConfigManager();
                default:
                    return null;
            }
        }
    }
}