using Script.Logic.Manager;
using Script.Manager;
using Script.Object;
using Script.Scene;
using UnityEngine;

namespace Script
{
    public class InputManager : BaseManager
    {
        private GameScene gameScene;

        public override void Init()
        {
            Debug.Log("InputManager Init");
            var sceneManager = GameCore.GetInstance().GetManager<SceneManager>();
            gameScene = sceneManager.gameScene;
        }

        public override void Update()
        {
            ListenHorMoveInput();
        }

        private void ListenHorMoveInput()
        {
            var playerUnit = gameScene.mainPlayer;
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                // 按下←
                playerUnit.operateLeft = true;
            }

            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                // 按下→
                playerUnit.operateRight = true;
            }

            if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A))
            {
                // 抬起←
                playerUnit.operateLeft = false;
            }

            if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D))
            {
                // 抬起→
                playerUnit.operateRight = false;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                // 开始
                gameScene.Start();
            }
        }

        public InputManager(ManagerType managerType) : base(managerType)
        {
        }
    }
}