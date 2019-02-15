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
using VARP.DataStructures.Interfaces;

namespace VARP.MVC
{
    public abstract class Representation : MonoBehaviour, IValidate
    {
        /// <summary>Reference to the entity can be used for pooling state</summary>
        [HideInInspector] public Entity entity;

        /// <summary>Last fixed frequency time</summary>
        public float lastChangedTime;

        /// <summary>Set true to apply interpolation</summary>
        public bool mustInterpolate;

        /// <summary>To terminate this object set the SpawnState.Despaw</summary>
        public SpawnState spawnState;

        // =============================================================================================================
        // Baking and Validation
        // =============================================================================================================

        // ReSharper disable once Unity.RedundantEventFunction
        /// <summary>
        ///     Override it for making validation of this object
        /// </summary>
        public virtual void OnValidate()
        {
        }

        // =============================================================================================================
        // Updating
        // =============================================================================================================

        /// <summary>
        ///     Function called by Entity::PreUpdate to store the previous state
        ///     of the EntityRepresentation for interpolation
        /// </summary>
        /// <param name="fixedFrequencyTime"></param>
        public abstract void PreUpdate(float fixedFrequencyTime);

        /// <summary>
        ///     Function called by the Representation manager to update the state
        ///     of the EntityRepresentation
        /// </summary>
        /// <param name="realTime"></param>
        public abstract void OnUpdate(float realTime);

        // =============================================================================================================
        // Event handling
        // =============================================================================================================

        /// <summary>
        ///     Usually, Entity Representation pooling the state of Entity to update own state.
        ///     For some special cases would be preferable use event method.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        /// <returns></returns>
        public virtual Result OnEntityEvent(EventTag id, object arg1 = null, object arg2 = null)
        {
            return Result.Null;
        }

        // =============================================================================================================
        // Spawning
        // =============================================================================================================

        /// <summary>
        ///     Called single time when when model received OnSpawn method
        /// </summary>
        /// <param name="injector"></param>
        public virtual void OnPreSpawn(object injector)
        {
            if (injector is Entity)
                entity = injector as Entity;
        }

        /// <summary>
        ///     Called single time after model received OnSpawn spawned
        /// </summary>
        public virtual void OnSpawn()
        {
        }

        // =============================================================================================================
        // Destroying entity
        // =============================================================================================================

        /// <summary>
        ///     Destroy this representation
        /// </summary>
        public virtual void DestroyEntity()
        {
            if (spawnState == SpawnState.DeSpawn)
                return;
            spawnState = SpawnState.DeSpawn;
            Destroy(gameObject);
        }
    }
}