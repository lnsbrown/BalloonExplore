namespace Script.Manager
{
    public class ManagerFactory
    {
        /**
         * 创建Manager
         */
        public static IManager CreateManager(ManagerType managerType)
        {
            if (managerType == ManagerType.Animation)
            {
                return new AnimManager();
            }
            else if (managerType == ManagerType.Scene)
            {
                return new SceneManager();
            }
            else if (managerType == ManagerType.UI)
            {
                return new UIManager();
            }

            return null;
        }
    }
}