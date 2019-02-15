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

using System;
using NaughtyAttributes;
using UnityEngine;
using VARP.ArtPrimitives;
using VARP.DataStructures.Interfaces;
using VARP.FSM;

namespace VARP.MVC
{

    public abstract partial class Entity : MonoBehaviour, IValidate
    {

        /// <summary>
        ///     To terminate this object set true
        /// </summary>
        public SpawnState spawnState;
             
        // =============================================================================================================
        // Objects tree
        // =============================================================================================================
        
        
        /// <summary>
        ///     Pointer to the representation
        /// </summary>
        [BoxGroup("Representation")]
        public Representation representation;

        /// <summary>
        ///     Get representation with requested type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetRepresentation<T>() where T : Representation
        {
            return representation as T;
        }

        // =============================================================================================================
        // Organize by tree
        // =============================================================================================================

        /// <summary>
        ///     Zone of this object
        /// </summary>
        [BoxGroup("World")]
        public Zone zone;
        /// <summary>
        ///     Pointer to the spawn-er
        /// </summary>
        [BoxGroup("World")]
        public Spawner spawner;

        // =============================================================================================================
        // Other stuff
        // =============================================================================================================
        
        /// <summary>
        ///     Pointer to rigid body
        /// </summary>
        [BoxGroup("Components")]
        public new Rigidbody rigidbody;


        [BoxGroup("Debugging")]
        public bool drawDebugInfo;

        // =============================================================================================================
        // Methods
        // =============================================================================================================

        /// <summary>
        ///     The process of this entity
        /// </summary>
        [NonSerialized]
        public FsmProcess process;
        /// <summary>
        ///     The resource of this entity
        /// </summary>
        [NonSerialized]
        public FsmResource resource;
        
        /// <summary>
        ///     Get the resource of this entity, cast it to requested type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetResource<T>() where T : FsmResource { return representation as T; }
        
        // =============================================================================================================
        // Methods
        // =============================================================================================================

        /// <summary>
        ///     Override this method for object which requires baking feature
        /// </summary>
        public virtual void OnValidate()
        {
            rigidbody = GetComponent<Rigidbody>();
            representation = GetComponentInChildren<Representation>();
            Debug.Assert(rigidbody != null);
        }
        
        // =============================================================================================================
        // Methods
        // =============================================================================================================

        /// <summary>
        ///     Function called by the EntityManager after PreUpdate to
        ///     of the Entity for interpolation
        /// </summary>
        /// <param name="fixedFrequencyTime"></param>
        public abstract void PreUpdate(float fixedFrequencyTime);

        /// <summary>
        ///     Function called by the EntityManager after PreUpdate to
        ///     update the state of the Entity
        /// </summary>
        /// <param name="fixedFrequencyTime"></param>
        public abstract void OnUpdate(float fixedFrequencyTime);

        // =============================================================================================================
        // Spawning process
        // =============================================================================================================

        /// <summary>
        ///     Called multiple times immediately after spawning
        /// </summary>
        /// <param name="injector"></param>
        public virtual void OnPreSpawn(object injector)
        {
            if (injector is FsmResource)
                resource = injector as FsmResource;
            if (injector is Spawner)
            {
                spawner = injector as Spawner;
                zone = spawner.zone;
                ApplyFacts(spawner.facts);
                return;
            }
            if (injector is Locator)
            {
                var locator = injector as Locator;
                zone = locator.zone;
                ApplyFacts(locator.facts);
                return;
            }
        }

        /// <summary>
        ///     After children will be instantiated
        /// </summary>
        public virtual void OnSpawn()
        {
            representation = GetComponentInChildren<Representation>();
            currentWorldTransform = transform;
            ForgetPreviousTransforms();
        }
        
        // =============================================================================================================
        // Destroying entity
        // =============================================================================================================

        public virtual void DestroyEntity()
        {
            if (spawnState.HasFlag(SpawnState.DeSpawn))
                return;
            representation?.DestroyEntity();
            spawnState = SpawnState.DeSpawn;
            Destroy(gameObject);
        }
        
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
        public virtual Result OnEvent(EventTag id, object arg1 = null, object arg2 = null)
        {
            return Result.Null;
        }
        
        // =============================================================================================================
        // Hi level language
        // =============================================================================================================

        /// <summary>
            /// The spawn point may know some additional facts
        /// </summary>
        /// <param name="facts"></param>
        public virtual void ApplyFacts(Fact[] facts)
        {
            if (facts == null) return;
            for (var i=0; i<facts.Length; i++) ApplyFact(facts[i]);
        }
        
        /// <summary>
        ///     Add additional fact to this entity
        /// </summary>
        /// <param name="facts"></param>
        public virtual void ApplyFact(Fact facts)
        {
            // nothing
        }
    }

}