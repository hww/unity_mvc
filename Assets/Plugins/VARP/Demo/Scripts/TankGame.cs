using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VARP.MVC
{
	public class TankGame : MonoBehaviour {

		private void FixedUpdate()
		{
			EntityManager.Update(Time.fixedTime, BucketTag.Defaults);
		}

		void Update () 
		{
			RepresentationManager.Update(Time.time, BucketTag.Defaults);
		}
	}
}
