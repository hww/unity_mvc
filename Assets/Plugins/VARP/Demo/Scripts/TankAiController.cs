using UnityEngine;

namespace VARP.MVC
{
    public class TankAiController : TankController
    {
        public override float GetAcceleration()
        {
            return 0;
        }

        public override float GetSteerDirection()
        {
            return 0;  
        }

        public override Vector3 GetAimTarget()
        {
            return new Vector3(0,0,0);  
        }

        public override bool Fire()
        {
            return false;
        }
    }
}