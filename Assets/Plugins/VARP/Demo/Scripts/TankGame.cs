using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VARP.MVC
{
	public class TankGame : MonoBehaviour {

		void Start () {
		
		}

		private void FixedUpdate()
		{
			EntryManager.Update(Time.fixedTime);
		}

		void Update () 
		{
			RepresentationManager.Update(Time.time);
		}
	}
}
