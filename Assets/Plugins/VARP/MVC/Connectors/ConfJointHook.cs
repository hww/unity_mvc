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
    public class ConfJointHook : AttachHook
    {
        /// <summary>Joint of this hook</summary>
        public ConfigurableJoint joint;
        
        /// <summary>
        ///     Create hook of this type
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="name"></param>
        public ConfJointHook(Entity entity, string name = null) : base(entity, name)
        {
        }

        /// <summary>
        /// 	Define hook
        /// </summary>
        /// <param name="joint">Joint of the model</param>
        /// <param name="transform">Transform of representation</param>
        public void Define(ConfigurableJoint joint, Transform transform)
        {
            if (this.joint != joint || this.transform != transform)
            {
                Disconnect();
                this.joint = joint;
                this.transform = transform;    
            }
        }

        /// <summary>Get joint's position</summary>
        public override Vector3 GetModelPosition()
        {
            return joint.transform.position;
        }
        /// <summary>Get joint's rotation</summary>
        public override Quaternion GetModelRotation()
        {
            return joint.transform.rotation;
        }

        // =============================================================================================================
        // Connect or disconnect
        // =============================================================================================================
        
        public override void Connect(AttachHook target, AttachOptions options = AttachOptions.Default)
        {
            Debug.Assert(target != null);
            Disconnect();
            OnConnect(target, options);
            target.OnConnect(this, options);
        }

        public override void Disconnect()
        {
            if (connectedHook==null)
                return;
            connectedHook.OnDisconnect();
            OnDisconnect();
        }
        
        // =============================================================================================================
        // Events when connection disconnection happens
        // =============================================================================================================
        
        public override void OnConnect(AttachHook target, AttachOptions options = AttachOptions.Default)
        {
            if (target is RigidbodyHook)
            {
                OnConnect(target as RigidbodyHook, options);
                return;
            }
            Debug.LogError($"Can't connect {this} to {target}");
        }

        private void OnConnect(RigidbodyHook target, AttachOptions options = AttachOptions.Default)
        {
            if (options.HasFlag(AttachOptions.Move))
                joint.transform.position = target.rigidbody.position;
            if (options.HasFlag(AttachOptions.Rotate))
                joint.transform.rotation = target.rigidbody.rotation;
            joint.connectedBody = target.rigidbody;
            joint.massScale = target.rigidbody.mass / joint.GetComponent<Rigidbody>().mass;
            joint.connectedMassScale = 1f;
            this.connectedHook = target;
            this.isConnected = true;
            entity.OnEvent(EventTag.OnAttachHookConnected, this, target);
        }
        
        public override void OnDisconnect()
        {
            connectedHook = null;
            joint.connectedBody = null;
            this.isConnected = false;
            entity.OnEvent(EventTag.OnAttachHookDisconnected, this);
        }
        
    }
}