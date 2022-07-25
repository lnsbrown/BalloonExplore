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
            var playerUnit = gameScene.mainPlayer as PlayerUnit;

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                // 按下←
                playerUnit.operateLeft = true;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                // 按下→
                playerUnit.operateRight = true;
            }
            else if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                // 抬起←
                playerUnit.operateLeft = false;
            }
            else if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                // 抬起→
                playerUnit.operateRight = false;
            }
        }

        public InputManager(ManagerType managerType) : base(managerType)
        {
        }
    }
}