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

namespace VARP.MVC.Connectors
{
    /// <summary>
    ///     AttachTarget can be connected with AttachHook. Like a parent child
    ///     relations but without using Unity transform hierarchy
    /// </summary>
    public abstract class AttachTarget
    {
        /// <summary>The target's entity</summary>
        public readonly Entity entity; 
        /// <summary>The name of target if need</summary>
        public readonly string name; 
        /// <summary>Has TRUE when connected</summary>
        public bool isConnected;
        /// <summary>Representation transform or null</summary>
        public Transform transform;

        /// <summary>
        ///     Create attach target
        /// </summary>
        /// <param name="entity">Entity pointer</param>
        /// <param name="name">Name of target</param>
        public AttachTarget(Entity entity, string name)
        {
            this.name = name;
            this.entity = entity;
        }

        /// <summary>
        ///     Get representation position
        /// </summary>
        public Vector3 RepPosition => transform.position;
        public Quaternion RepRotation => transform.rotation;

        // =============================================================================================================
        // Connect or disconnect
        // =============================================================================================================

        /// <summary>
        ///     Connect to the hook
        /// </summary>
        /// <param name="hook"></param>
        /// <param name="options"></param>
        public abstract void Connect(AttachHook hook, AttachOptions options = AttachOptions.Default);

        /// <summary>
        ///     Disconnect this target
        /// </summary>
        public abstract void Disconnect();

        // =============================================================================================================
        // Events when connection disconnection happens
        // =============================================================================================================

        /// <summary>
        ///     OnConnect event, same as Connect but do not call attach hook
        /// </summary>
        public abstract void OnConnect(AttachHook hook, AttachOptions options = AttachOptions.Default);

        /// <summary>
        ///     OnDisconnect event, same as Disconnect but do not call attach hook
        /// </summary>
        public abstract void OnDisconnect();
    }
}