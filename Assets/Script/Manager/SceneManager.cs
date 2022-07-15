using Script.Manager;
using Script.Scene;
using UnityEngine;

namespace Script
{
    public class SceneManager : IManager
    {
        private GameScene gameScene;

        public void Init()
        {
            Debug.Log("SceneManager Init");
            gameScene = new GameScene();
            gameScene.Init();
        }

        public void Update()
        {
            gameScene.Update();
        }
    }
}