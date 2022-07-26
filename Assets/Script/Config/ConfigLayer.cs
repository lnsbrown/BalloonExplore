using System;

namespace Script.Config
{
    /// <summary>
    /// 层级表，指定游戏对象在场景中的层级关系，越小的层级在越上面
    /// </summary>
    [Serializable]
    public class ConfigLayer
    {
        /// <summary>
        /// 背景层级
        /// </summary>
        public int bgMap;


        /// <summary>
        /// 玩家层级
        /// </summary>
        public int player;
    }
}