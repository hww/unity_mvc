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

namespace VARP.MVC.Connectors
{
    public class RigidbodyTarget : AttachTarget
    {
        /// <summary>Attached hook</summary>
        public AttachHook hook;
        /// <summary>Rigidbody of this target</summary>
        public Rigidbody rigidbody;

        /// <summary>
        ///     Construct this target
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="name"></param>
        public RigidbodyTarget(Entity entity, string name = null) : base(entity, name)
        {
        }

        /// <summary>
        ///     Define this target
        /// </summary>
        /// <param name="rigidbody">Rigidbody of model</param>
        /// <param name="transform">Transform of representation</param>
        public void Define(Rigidbody rigidbody, Transform transform)
        {
            this.rigidbody = rigidbody;
            this.transform = transform;
        }

        // =============================================================================================================
        // Connect or disconnect
        // =============================================================================================================

        public override void Connect(AttachHook hook, AttachOptions options = AttachOptions.Default)
        {
            Debug.Assert(hook != null);
            Disconnect();
            OnConnect(hook, options);
            hook.OnConnect(this, options);
        }

        public override void Disconnect()
        {
            if (hook == null)
                return;
            hook.OnDisconnect();
            OnDisconnect();
        }

        // =============================================================================================================
        // Events when connection disconnection happens
        // =============================================================================================================

        public override void OnConnect(AttachHook hook, AttachOptions options = AttachOptions.Default)
        {
            if (hook is ConfJointHook)
                OnConnectHook(hook as ConfJointHook, options);
            Debug.LogError($"Can't connect {this} to {hook}");
        }

        private void OnConnectHook(ConfJointHook hook, AttachOptions options = AttachOptions.Default)
        {
            this.hook = hook;
            isConnected = true;
            entity.OnEvent(EventTag.OnAttachTargetConnected, this, hook);
        }

        public override void OnDisconnect()
        {
            hook = null;
            isConnected = false;
            entity.OnEvent(EventTag.OnAttachTargetDisconnected, this);
        }
    }
}