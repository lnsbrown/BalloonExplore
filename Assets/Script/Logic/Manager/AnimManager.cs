using Script.Logic.Manager;
using Script.Manager;
using UnityEngine;

namespace Script
{
    public class AnimManager : BaseManager
    {
        public AnimManager(ManagerType managerType) : base(managerType)
        {
        }

        public override void Init()
        {
            Debug.Log("AnimManager Init");
        }

        public override void Update()
        {
        }
    }
}