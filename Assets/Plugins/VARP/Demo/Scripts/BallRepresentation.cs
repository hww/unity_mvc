using UnityEngine;

namespace VARP.MVC
{
    public class BallRepresentation : Representation
    {
        private void OnEnable()
        {
            spawnState = SpawnState.Ready;
            RepresentationManager.AddRepresentation(this, BucketTag.Defaults);
        }

        private void OnDisable()
        {
            spawnState = SpawnState.None;
        }
        
        /// <summary>        
        /// Function called by Entity.PreUpdate to store the previous state
        /// of the EntityRepresentation for interpolation
        /// </summary>
        public override void PreUpdate(float fixedFrequencyTime)
        {
            // This flag indicates of for the next 1/15th second the EntityRepresentation needs to interpolate
            mustInterpolate = entity.changed;
            // Note the current time
            lastChangedTime = fixedFrequencyTime;
        } 
        
        /// <summary>       
        /// Function called by the Representation manager to update the state
        /// of the EntityRepresentation
        /// </summary>
        public override void OnUpdate(float RealTime)
        {
            if (mustInterpolate)
            {
                // Calculate the interpolation factor, the factor will be in the range [0, 1]
                var factor = Mathf.Min((RealTime - lastChangedTime) / Time.fixedDeltaTime, 1f);
                // Use spherical linear interpolation for the world matrix
                entity.InterpolateSlerp(transform, factor);
            }
            else
            {
                // We’re not interpolating so we can simply use the current Entity statie
                entity.InterpolateApply(transform);
            }
        } 

    }

}