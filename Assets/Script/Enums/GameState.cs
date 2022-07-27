using System.Collections.Generic;
using System.Linq;

namespace Script.Enums
{
    public enum GameState
    {
        // 初始
        Init,

        // 游戏
        Gaming,

        // 暂停
        Pause,

        // 结算
        Settle,

        // 结束
        Over
    }

    public class GameStateTool
    {
        private static Dictionary<GameState, GameState[]> preStates = new Dictionary<GameState, GameState[]>();

        static GameStateTool()
        {
            preStates.Add(GameState.Init, new[] { GameState.Pause });
            preStates.Add(GameState.Gaming, new[] { GameState.Init, GameState.Pause });
            preStates.Add(GameState.Pause, new[] { GameState.Gaming, GameState.Init });
            preStates.Add(GameState.Settle, new[] { GameState.Gaming });
            preStates.Add(GameState.Over, new[] { GameState.Settle });
        }

        /// <summary>
        /// 是否可进入状态
        /// </summary>
        /// <param name="curState"></param>
        /// <param name="enterState"></param>
        /// <returns></returns>
        public static bool CanEnter(GameState curState, GameState enterState)
        {
            return preStates[enterState].Contains(curState);
        }
    }
}