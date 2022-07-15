using UnityEngine;

namespace Script.Object
{
    public abstract class GameUnit : ScriptableObject
    {
        public abstract void Init();
        public abstract void Update();
    }
}