using UnityEngine;

namespace VARP.MVC
{
    public class TankRepresentation : EntityRepresentation
    {
        private void OnEnable()
        {
            Despawn = false;
            RepresentationManager.AddRepresentation(this);
        }

        private void OnDisable()
        {
            Despawn = true;
        }
        
        /// <summary>        
        /// Function called by Entity.PreUpdate to store the previous state
        /// of the EntityRepresentation for interpolation
        /// </summary>
        public override void PreUpdate(float FixedFrequencyTime)
        {
            // This flag indicates of for the next 1/15th second the EntityRepresentation needs to interpolate
            MustInterpolate = Entity.Changed;
            // Note the current time
            LastChangedTime = FixedFrequencyTime;
        } 
        
        /// <summary>       
        /// Function called by the Representation manager to update the state
        /// of the EntityRepresentation
        /// </summary>
        public override void OnUpdate(float RealTime)
        {
            if (MustInterpolate)
            {
                // Calculate the interpolation fractor, the factor will be in the range [0, 1]
                var factor = Mathf.Min((RealTime - LastChangedTime) / Time.fixedDeltaTime, 1f);
                // Use spherical linear interpolation for the world matrix
                Entity.InterpolateSLERP(transform, factor);
                // Use linear blending for the array of bone matrices
                // TODO BoneMatrices = (1f – factor) *Entity->PreviousBoneMatrices + factor * Entity.CurrentBoneMatrices;
            }
            else
            {
                // We’re not interpolating so we can simply use the current Entity statie
                Entity.InterpolateApply(transform);
                // TODO BoneMatrices = Entity.CurrentBoneMatrices;
            }
        } 

    }

}