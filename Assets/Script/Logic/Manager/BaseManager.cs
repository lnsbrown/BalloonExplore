using System;
using Script.Manager;
using UnityEngine;

namespace Script.Logic.Manager
{
    public abstract class BaseManager : IManager
    {
        public ManagerType managerType;

        protected BaseManager(ManagerType managerType)
        {
            this.managerType = managerType;
        }

        public abstract void Init();
        public abstract void Update();
    }
}