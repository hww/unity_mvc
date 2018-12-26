using UnityEngine;

namespace VARP.MVC
{
    public abstract class EntityRepresentation : MonoBehaviour
    {
        /// <summary>To terminate this object set true</summary>
        public bool Despawn;
        /// <summary>To terminate this object set true</summary>
        public bool Changed;
        /// <summary>To terminate this object set true</summary>
        public bool MustInterpolate;
        /// <summary>Last fixed frequency time</summary>
        public float LastChangedTime;
        /// <summary>Reference to the entity can be used for pooling state</summary>
        public Entity Entity;
        
        /// <summary>        
        /// Function called by Entity::PreUpdate to store the previous state
        /// of the EntityRepresentation for interpolation
        /// </summary>
        public abstract void PreUpdate(float FixedFrequencyTime);

        /// <summary>       
        /// Function called by the Representation manager to update the state
        /// of the EntityRepresentation
        /// </summary>
        public abstract void OnUpdate(float RealTime);
        
        /// <summary>
        /// Ususaly, Entity Representation pooling the state of Entity to update own state.
        /// For some special cases would be prefferable use event method.
        /// </summary>
        public virtual void OnEntityEvent(int id, object arg1 = null, object arg2 = null)
        {
            // override it in your representaion
        }
    }
}