using Script.Object;
using UnityEngine;

namespace Script.Scene
{
    public class GameScene
    {
        private GameUnit bgUnit;

        public void Init()
        {
            bgUnit = ScriptableObject.CreateInstance<BgUnit>();
            bgUnit.Init();
        }

        public void Update()
        {
            bgUnit.Update();
        }
    }
}