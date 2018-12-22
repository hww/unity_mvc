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

        public float LastChangedTime;
        
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
        
    }
}