using UnityEngine;

namespace VARP.MVC
{
    public class TankKeyboardController : TankController
    {
        public override float GetAcceleration()
        {
            var acceleration = 0;
            if (Input.GetKey(KeyCode.W)) acceleration++;
            if (Input.GetKey(KeyCode.S)) acceleration--;
            return acceleration;
        }

        public override float GetSteerDirection()
        {
            var y = 0;
            if (Input.GetKey(KeyCode.A)) y--;
            if (Input.GetKey(KeyCode.D)) y++;
            return y;
        }

        public override Vector3 GetAimTarget()
        {
            var x = 0;
            var y = 0;
            if (Input.GetKey(KeyCode.LeftArrow)) x--;
            if (Input.GetKey(KeyCode.RightArrow)) x++;
            if (Input.GetKey(KeyCode.DownArrow)) y--;
            if (Input.GetKey(KeyCode.UpArrow)) x++;
            return new Vector3(x,y,0);
        }

        public override bool Fire()
        {
            return Input.GetKeyDown(KeyCode.Space);
        }
    }
}