using UnityEngine;

namespace VARP.MVC
{
    public abstract class Entity : MonoBehaviour
    {
        public enum EChange
        {
            None,
            CurrentWorldMatrixChanged,
            CurrentBoneMatricesChanged
        }
        /// <summary>Interpolate transform when this flag is true</summary>
        public EChange Changed;
        /// <summary>To terminate this object set true</summary>
        public bool Despawn;
        /// <summary>Pointer to the representation</summary>
        public EntityRepresentation EntityRepresentation;
        /// <summary>Transform data states</summary>
        protected TransformData[] lastTransforms;
        /// <summary>Transfrom data index</summary>
        protected int newTransformIndex;
        /// <summary>Get old transform index</summary>
        protected int OldTransformIndex() { return (newTransformIndex == 0 ? 1 : 0); }

        /// <summary>
        /// Function called by the EntityManager after PreUpdate to
        /// of the Entity for interpolation
        /// </summary>
        public abstract void PreUpdate(float FixedFrequencyTime);
        /// <summary>
        /// Function called by the EntityManager after PreUpdate to
        /// update the state of the Entity
        /// </summary>
        public abstract void OnUpdate(float FixedFrequencyTime);
        
        /// <summary>Reset the transform</summary>
        public void ForgetPreviousTransforms() {
            lastTransforms = new TransformData[2];
            TransformData t = new TransformData(transform.localPosition, transform.localRotation, transform.localScale);
            lastTransforms[0] = t;
            lastTransforms[1] = t;
            newTransformIndex = 0;
        }

        /// <summary>Interpolate last tansforms and apply to target</summary>
        public void InterpolateSLERP(Transform targetTransf, float factor)
        {
            TransformData newestTransform = lastTransforms[newTransformIndex];
            TransformData olderTransform = lastTransforms[OldTransformIndex()];
            targetTransf.localPosition = Vector3.Lerp(olderTransform.position,  newestTransform.position, factor);
            targetTransf.localRotation = Quaternion.Slerp(olderTransform.rotation, newestTransform.rotation, factor);
            targetTransf.localScale = Vector3.Lerp(olderTransform.scale, newestTransform.scale, factor);
        }

        /// <summary>Apply last tansform without interpolation</summary>
        public void InterpolateApply(Transform targetTransf)
        {
            TransformData newestTransform = lastTransforms[newTransformIndex];
            targetTransf.localPosition = newestTransform.position;
            targetTransf.localRotation = newestTransform.rotation;
            targetTransf.localScale = newestTransform.scale;
        }
    }
}