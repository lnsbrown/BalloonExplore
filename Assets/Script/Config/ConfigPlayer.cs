using System;

namespace Script.Config
{
    [Serializable]
    public class ConfigPlayer
    {
        /// <summary>
        /// 气球宽度
        /// </summary>
        public float balloonWidth;

        /// <summary>
        /// 气球悬空高度
        /// </summary>
        public float balloonHoverHeight;

        /// <summary>
        /// 操作向左加速度
        /// </summary>
        public float operateLeftAccelerate;

        /// <summary>
        /// 操作向右加速度
        /// </summary>
        public float operateRightAccelerate;

        /// <summary>
        /// 阻碍加速度（模拟空气阻力）
        /// </summary>
        public float blockAccelerate;
    }
}