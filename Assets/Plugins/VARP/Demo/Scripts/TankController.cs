using UnityEngine;

namespace VARP.MVC
{
    public abstract class TankController : EntityController
    {
        public abstract float GetAcceleration();
        public abstract float GetSteerDirection();
        public abstract Vector3 GetAimTarget();
        public abstract bool Fire();
    }
}