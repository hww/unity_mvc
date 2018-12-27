using UnityEngine;

namespace VARP.MVC
{
    public abstract class TankController : EntityController
    {
        /// <summary>Called per frame, return best possible weapon</summary>
        public abstract Entity GetUseObject ();
        /// <summary>Called per frame, return acceleration</summary>
        public abstract float GetAcceleration();
        /// <summary>Called per frame, return steering direction</summary>
        public abstract float GetSteerDirection();
        /// <summary>Called per frame, return aim target</summary>
        public abstract Vector3 GetAimTarget();
    }
}