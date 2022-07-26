using UnityEngine;

namespace Script.Object
{
    public abstract class GameUnit : ScriptableObject
    {
        public int unitId { set; get; }

        /// <summary>
        /// 场景层级
        /// </summary>
        protected int unitSceneLayerIndex { get; private set; }

        // 单位对象
        protected GameObject gameObject;

        protected abstract void InitGameObject();

        /// <summary>
        /// 获取场景层级
        /// </summary>
        /// <returns>场景层级</returns>
        protected abstract int GetSceneLayerIndex();

        public virtual void Init()
        {
            unitSceneLayerIndex = GetSceneLayerIndex();
            InitGameObject();
        }

        public abstract void Update();
    }
}