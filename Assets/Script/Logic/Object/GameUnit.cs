using Script.Scene;
using UnityEngine;

namespace Script.Object
{
    public abstract class GameUnit : ScriptableObject
    {
        public bool needRemove;

        public BaseScene scene { set; get; }

        public int unitId { set; get; }

        /// <summary>
        /// 场景层级
        /// </summary>
        protected int unitSceneLayerIndex { get; private set; }

        /// <summary>
        /// 单位对象
        /// </summary>
        public GameObject gameObject { get; protected set; }

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

        public virtual void OnRemove()
        {
            // 销毁对象
            Destroy(gameObject);
        }
    }
}