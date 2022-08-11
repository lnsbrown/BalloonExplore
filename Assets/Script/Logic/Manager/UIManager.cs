using Script.Logic.Manager;
using Script.Manager;
using UnityEngine;

namespace Script
{
    public class UIManager : BaseManager
    {
        public override void Init()
        {
            Debug.Log("UIManager Init");
        }

        public override void Update()
        {
        }

        public UIManager(ManagerType managerType) : base(managerType)
        {
        }
    }
}