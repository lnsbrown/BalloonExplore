using System;
using UnityEngine;

namespace Script.Manager
{
    public class TimeManager : IManager
    {
        // 当前时间戳
        private long curTimestamp;

        private float cachePeriod;

        public void Init()
        {
            Debug.Log("TimeManager Init");
            cachePeriod = Globals.configGlobal.cacheTimeMillSecond / 1000f;
            UpdateTimestamp();
        }

        private long nextUpdateTime = 0;

        private float updateTimeAcculate = 0;

        public void Update()
        {
            updateTimeAcculate += Time.deltaTime;
            if (updateTimeAcculate >= cachePeriod)
            {
                updateTimeAcculate = 0;
                UpdateTimestamp();
            }
        }

        public void UpdateTimestamp()
        {
            curTimestamp = GetTimeStampMilliSecond();
        }

        /// <summary>
        /// 获取时间戳-单位毫秒
        /// </summary>
        /// <returns></returns>
        private long GetTimeStampMilliSecond()
        {
            // 621355968000000000 是格林威治1970-1-1的时间
            return (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000;
        }

        /// <summary>
        /// 获取当前时间戳ms
        /// </summary>
        /// <returns></returns>
        public long Now()
        {
            if (Globals.configGlobal.useCacheTime)
            {
                return this.curTimestamp;
            }

            return GetTimeStampMilliSecond();
        }
    }
}