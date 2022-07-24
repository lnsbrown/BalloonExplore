using UnityEngine;

namespace Script.Object
{
    public abstract class GameUnit : ScriptableObject
    {
        public int unitId { set; get; }

        // 单位对象
        protected GameObject gameObject;

        protected abstract void InitGameObject();

        public virtual void Init()
        {
            InitGameObject();
        }

        public abstract void Update();
    }
}