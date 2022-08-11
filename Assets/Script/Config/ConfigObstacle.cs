using System;

namespace Script.Config
{
    [Serializable]
    public class ConfigObstacle
    {
        /// <summary>
        /// 最小障碍空隙
        /// </summary>
        public float obstacleMinSpace;

        /// <summary>
        /// 障碍物宽度
        /// </summary>
        public float obstacleWidth;

        /// <summary>
        /// 障碍物高度
        /// </summary>
        public float obstacleHeight;

        /// <summary>
        ///  创建间隔时间(s)
        /// </summary>
        public long createInterval;
    }
}