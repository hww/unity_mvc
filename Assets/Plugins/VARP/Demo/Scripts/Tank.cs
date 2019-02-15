﻿using UnityEngine;

namespace VARP.MVC
{
    public class Tank : Entity
    {
        public TankController Controler;

        private Rigidbody rigidbody;
        
        private void OnEnable()
        {
            rigidbody = GetComponent<Rigidbody>();
            Controler = new TankKeyboardController();
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
            StoreBoneTransform();
            // Notify our EntityRepresentation
            representation.PreUpdate(fixedFrequencyTime);
            // Reset changed flag for next frame
            changed = false;
        }

        public float speed = 3;
        public float turnSpeed = 180;
        
        /// <summary>
        /// Function called by the EntityManager after PreUpdate to
        /// update the state of the Entity
        /// </summary>
        public override void OnUpdate(float FixedFrequencyTime)
        {
            // Update game state (animations, position, etc.)
            var direction = Controler.GetSteerDirection();
            // Determine the number of degrees to be turned based on the input, speed and time between frames.
            var turn = direction * turnSpeed * Time.fixedDeltaTime;
            // Make this into a rotation in the y axis.
            Quaternion turnRotation = Quaternion.Euler (0f, turn, 0f);
            // Apply this rotation to the rigid body's rotation.
            rigidbody.MoveRotation (rigidbody.rotation * turnRotation);
            
            // Get acceleration from controller
            var acceleration = Controler.GetAcceleration();
            // Create a vector in the direction the tank is facing with a magnitude based on the input, speed and the time between frames.
            var movement = transform.forward * acceleration * speed * Time.fixedDeltaTime;
            // Apply this movement to the rigid body's position.
            rigidbody.MovePosition(rigidbody.position + movement);

            // The EntityRepresentation needs to know if anything changed this frame
            // that requires interpolation
            changed = true;
        } 
    }
}