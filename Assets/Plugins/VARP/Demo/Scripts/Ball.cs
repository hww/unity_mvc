using UnityEngine;

namespace VARP.MVC
{
    public class Ball : Entity
    {
        private Rigidbody rigidbody;
        
        private void OnEnable()
        {
            rigidbody = GetComponent<Rigidbody>();
            representation.OnPreSpawn(this);
            currentWorldTransform = transform;
            spawnState = SpawnState.Ready;
            representation.transform.parent = null;
            EntityManager.AddEntity(this, BucketTag.Defaults);
        }

        private void OnDisable()
        {
            spawnState = SpawnState.DeSpawn;
        }

        /// <summary>
        /// Function called by the EntityManager to store the previous state 
        /// of the Entity for interpolation
        /// </summary>
        public override void PreUpdate(float fixedFrequencyTime)
        {
            // Store the previous world matrix
            StoreWorldTransform();
            // Notify our EntityRepresentation
            representation.PreUpdate(fixedFrequencyTime);
            // Reset changed flag for next frame
            changed = false;
        }
        
        /// <summary>
        /// Function called by the EntityManager after PreUpdate to
        /// update the state of the Entity
        /// </summary>
        public override void OnUpdate(float FixedFrequencyTime)
        {
            // Get acceleration from controller
            var velocity = rigidbody.velocity;
            // The EntityRepresentation needs to know if anything changed this frame
            // that requires interpolation
            if (velocity.sqrMagnitude != 0)
                changed = true;
        } 
    }
}