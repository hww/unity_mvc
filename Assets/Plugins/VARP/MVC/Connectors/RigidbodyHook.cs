// =============================================================================
// MIT License
// 
// Copyright (c) 2018 Valeriya Pudova (hww.github.io)
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
// =============================================================================

using UnityEngine;
using VARP.FSM;
using VARP.MVC;

namespace VARP.MVC.Connectors
{
	public class RigidbodyHook : AttachHook
	{
		/// <summary>Rigidbody of this hook</summary>
		public Rigidbody rigidbody;	//< model

		/// <summary>
		///     Create hook of this type
		/// </summary>
		/// <param name="entity"></param>
		/// <param name="name"></param>
		public RigidbodyHook(Entity entity, string name = null) : base(entity, name)
		{
		}

		/// <summary>
		/// 	Define hook
		/// </summary>
		/// <param name="rigidbody">Rigidbody of the model</param>
		/// <param name="transform">Transform of representation</param>
		public void Define(Rigidbody rigidbody, Transform transform)
		{
			Disconnect();
			this.rigidbody = rigidbody;
			this.transform = transform;    
		}
		
		// =============================================================================================================
		// Connect or disconnect
		// =============================================================================================================
        
		public override void Connect(AttachTarget target, AttachOptions options = AttachOptions.Default)
		{
			Debug.Assert(target != null);
			Disconnect();
			OnConnect(target, options);
			target.OnConnect(this, options);
		}
		
		public override void Disconnect()
		{
			if (target == null)
				return;
			target.OnDisconnect();
			OnDisconnect();
		}

		// =============================================================================================================
		// Events when connection disconnection happens
		// =============================================================================================================
		
		public override void OnConnect(AttachTarget target, AttachOptions options = AttachOptions.Default)
		{
			if (target is RigidbodyTarget)
			{
				OnConnectRigidbody(target as RigidbodyTarget, options);
				return;
			}
			if (target is ConfJointTarget)
			{
				OnConnectConfJoint(target as ConfJointTarget, options);
				return;
			}
			Debug.LogError($"Can't connect {this} to {target}");
		}

		private void OnConnectRigidbody(RigidbodyTarget target, AttachOptions options = AttachOptions.Default)
		{
			this.target = target;
			this.isConnected = true;
			entity.OnEvent(EventTag.OnAttachHookConnected, this, target);
		}
		
		private void OnConnectConfJoint(ConfJointTarget target, AttachOptions options = AttachOptions.Default)
		{
			this.target = target;
			this.isConnected = true;
			entity.OnEvent(EventTag.OnAttachHookConnected, this, target);
		}
		
		public override void OnDisconnect()
		{
			target = null;
			this.isConnected = false;
			entity.OnEvent(EventTag.OnAttachHookDisconnected, this);
		}

	}
}