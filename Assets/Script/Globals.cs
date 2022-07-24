using Script.Config;
using Script.Manager;

namespace Script
{
    public static class Globals
    {
        public static ConfigGlobal configGlobal =>
            GameCore.GetInstance().GetManager<ConfigManager>().configGlobal;

        /// <summary>
        /// 获取当前时间戳
        /// </summary>
        /// <returns></returns>
        public static long Now()
        {
            return GameCore.GetInstance().GetManager<TimeManager>().Now();
        }
    }
}