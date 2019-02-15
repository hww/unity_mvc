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
    ///     AttachHook can be connected with AttachHook. Like a parent child
    ///     relations but without using Unity transform hierarchy
    /// </summary>
    public abstract class AttachHook
    {
        /// <summary>Hook's entity</summary>
        public readonly Entity entity; 
        /// <summary>Hook's name</summary>
        public readonly string name; 
        /// <summary>Return TRUE if connected</summary>
        public bool isConnected;
        /// <summary>Connected target</summary>
        public AttachHook connectedHook; 
        /// <summary>Hooks representation's transform</summary>
        public Transform transform; //< representation

        /// <summary>
        ///     Base attach hook
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="name"></param>
        public AttachHook(Entity entity, string name = null)
        {
            this.name = name;
            this.entity = entity;
        }

        // =============================================================================================================
        // Connect or disconnect
        // =============================================================================================================

        /// <summary>
        ///     Connect method
        /// </summary>
        /// <param name="target"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public abstract void Connect(AttachHook target, AttachOptions options = AttachOptions.Default);

        /// <summary>
        ///     Disconnect
        /// </summary>
        public abstract void Disconnect();

        // =============================================================================================================
        // Events when connection disconnection happens
        // =============================================================================================================

        /// <summary>
        ///     OnConnect event, same as Connect but do not call attach target
        /// </summary>
        public abstract void OnConnect(AttachHook target, AttachOptions options = AttachOptions.Default);

        /// <summary>
        ///     OnDisconnect event, same as Disconnect but do not call attach target
        /// </summary>
        public abstract void OnDisconnect();
        
        // =============================================================================================================
        // Access to position rotation
        // =============================================================================================================

        public abstract Vector3 GetModelPosition();
        public abstract Quaternion GetModelRotation();

        public Vector3 GetRepPosition()
        {
            return transform.position;
        }

        public Quaternion GetRepRotation()
        {
            return transform.rotation;
        }
    }
}