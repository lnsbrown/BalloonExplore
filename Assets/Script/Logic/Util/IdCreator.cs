using System.Collections.Generic;
using Script.Enums;

namespace Script.Util
{
    public class IdCreator
    {
        private static readonly IdCreator INSTANCE = new IdCreator();

        private IdCreator()
        {
        }

        public static IdCreator GetInstance()
        {
            return INSTANCE;
        }

        private readonly Dictionary<IdIncreaseType, int> idCounterDic = new Dictionary<IdIncreaseType, int>();

        public int Incr(IdIncreaseType idIncreaseType)
        {
            var idCounter = 0;
            if (idCounterDic.ContainsKey(idIncreaseType))
            {
                idCounter = idCounterDic[idIncreaseType];
            }

            idCounter += 1;
            idCounterDic[idIncreaseType] = idCounter;
            return idCounter;
        }
    }
}