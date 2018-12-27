using UnityEngine;

namespace VARP.MVC
{
    public class TankAiController : TankController
    {
        public override float GetAcceleration()
        {
            // TODO
            return 0;
        }

        public override float GetSteerDirection()
        {
            // TODO
            return 0;  
        }

        public override Vector3 GetAimTarget()
        {
            // TODO
            return new Vector3(0,0,0);  
        }

        public override Entity GetUseObject()
        {
            // TODO return current weapon entity when AI decide it
            return null;
        }
    }
}