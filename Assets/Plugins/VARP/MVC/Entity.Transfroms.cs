using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using VARP.DataStructures;

namespace VARP.MVC
{
    public abstract partial class Entity
    {
        // =============================================================================================================
        // Transform and interpolations
        // =============================================================================================================
                
        // Interpolate transform when this flag is true
        [NonSerialized]
        public bool changed;

        [NonSerialized]
        public Transform currentWorldTransform;
        private TransformData lastWorldTransforms;
        
        [NonSerialized]
        public Transform[] currentBoneTransforms;
        private TransformData[] lastBoneTransforms;
        
        /// <summary>
        ///     Reset the transform
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual void ForgetPreviousTransforms() {
            StoreWorldTransform();
            StoreBoneTransform();
        }

        /// <summary>
        ///     Interpolate last transforms and apply to target
        /// </summary>
        /// <param name="targetTransform"></param>
        /// <param name="factor"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void InterpolateSlerp(Transform targetTransform, float factor)
        {
            targetTransform.localPosition = Vector3.Lerp(lastWorldTransforms.localPosition,  currentWorldTransform.localPosition, factor);
            targetTransform.localRotation = Quaternion.Slerp(lastWorldTransforms.localRotation, currentWorldTransform.localRotation, factor);
            targetTransform.localScale = Vector3.Lerp(lastWorldTransforms.localScale, currentWorldTransform.localScale, factor);        
        }

        /// <summary>
        ///     Interpolate last transforms and apply to target
        /// </summary>
        /// <param name="targetTransform"></param>
        /// <param name="factor"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void InterpolateSlerpPR(Transform targetTransform, float factor)
        {
            targetTransform.localPosition = Vector3.Lerp(lastWorldTransforms.localPosition,  currentWorldTransform.localPosition, factor);
            targetTransform.localRotation = Quaternion.Slerp(lastWorldTransforms.localRotation, currentWorldTransform.localRotation, factor);
        }

        /// <summary>
        ///     Interpolate last transforms and apply to target
        /// </summary>
        /// <param name="targetTransform"></param>
        /// <param name="factor"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void InterpolateSlerpR(Transform targetTransform, float factor)
        {
            targetTransform.localRotation = Quaternion.Slerp(lastWorldTransforms.localRotation, currentWorldTransform.localRotation, factor);
        }
        
        /// <summary>
        ///     Apply last transform without interpolation
        /// </summary>
        /// <param name="targetTransform"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void InterpolateApply(Transform targetTransform)
        {
            targetTransform.localPosition = currentWorldTransform.localPosition;
            targetTransform.localRotation = currentWorldTransform.localRotation;
            targetTransform.localScale = currentWorldTransform.localScale;
        }
        
        /// <summary>
        ///     Apply last transform without interpolation
        /// </summary>
        /// <param name="targetTransform"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void InterpolateApplyPR(Transform targetTransform)
        {
            targetTransform.localPosition = currentWorldTransform.localPosition;
            targetTransform.localRotation = currentWorldTransform.localRotation;
        }
        
        /// <summary>
        ///     Apply last transform without interpolation
        /// </summary>
        /// <param name="targetTransform"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void InterpolateApplyR(Transform targetTransform)
        {
            targetTransform.localRotation = currentWorldTransform.localRotation;
        }
        
        /// <summary>
        ///     Store current transform
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void StoreWorldTransform()
        {
            lastWorldTransforms.localPosition = currentWorldTransform.localPosition;
            lastWorldTransforms.localRotation = currentWorldTransform.localRotation;
            lastWorldTransforms.localScale = currentWorldTransform.localScale;
        }
        
        /// <summary>
        ///     Store current bone transforms
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void StoreBoneTransform()
        {
            if (currentBoneTransforms == null)
                return;
            if (lastBoneTransforms == null || lastBoneTransforms.Length != currentBoneTransforms.Length)
                lastBoneTransforms = new TransformData[currentBoneTransforms.Length];
            for (var i = 0; i < currentBoneTransforms.Length; i++)
            {
                lastBoneTransforms[i] = new TransformData(
                    currentBoneTransforms[i].localPosition,
                    currentBoneTransforms[i].localRotation,
                    currentBoneTransforms[i].localScale);  
            }
        }
        
        /// <summary>
        ///     Interpolate last transforms and apply to target
        /// </summary>
        /// <param name="targetTransforms"></param>
        /// <param name="factor"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void InterpolateSlerp(Transform[] targetTransforms, float factor)
        {
            for (var i = 0; i < targetTransforms.Length; i++)
            {
                var cur = currentBoneTransforms[i];
                var tgt = targetTransforms[i];
                tgt.localPosition =
                    Vector3.Lerp(lastBoneTransforms[i].localPosition, cur.localPosition, factor);
                tgt.localRotation =
                    Quaternion.Slerp(lastBoneTransforms[i].localRotation, cur.localRotation, factor);
                tgt.localScale = 
                    Vector3.Lerp(lastBoneTransforms[i].localScale, cur.localScale, factor);
            }
        }
        
        /// <summary>
        ///     Interpolate last transforms and apply to target
        /// </summary>
        /// <param name="targetTransforms"></param>
        /// <param name="factor"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void InterpolateSlerpPR(Transform[] targetTransforms, float factor)
        {
            for (var i = 0; i < targetTransforms.Length; i++)
            {
                var cur = currentBoneTransforms[i];
                var tgt = targetTransforms[i];
                tgt.localPosition =
                    Vector3.Lerp(lastBoneTransforms[i].localPosition, cur.localPosition, factor);
                tgt.localRotation =
                    Quaternion.Slerp(lastBoneTransforms[i].localRotation, cur.localRotation, factor);
            }
        }
        
        /// <summary>
        ///     Interpolate last transforms and apply to target
        /// </summary>
        /// <param name="targetTransforms"></param>
        /// <param name="factor"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void InterpolateSlerpR(Transform[] targetTransforms, float factor)
        {
            for (var i = 0; i < targetTransforms.Length; i++)
            {
                var cur = currentBoneTransforms[i];
                var tgt = targetTransforms[i];
                tgt.localRotation =
                    Quaternion.Slerp(lastBoneTransforms[i].localRotation, cur.localRotation, factor);
            }
        }
        
        /// <summary>
        ///     Apply last transform without interpolation
        /// </summary>
        /// <param name="targetTransforms"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void InterpolateApply(Transform[] targetTransforms)
        {
            for (var i = 0; i < targetTransforms.Length; i++)
            {
                var src = currentBoneTransforms[i];
                var tgt = targetTransforms[i];
                tgt.localPosition = src.localPosition;
                tgt.localRotation = src.localRotation;
                tgt.localScale = src.localScale;
            }
        }
        
        /// <summary>
        ///     Apply last transform without interpolation
        /// </summary>
        /// <param name="targetTransforms"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void InterpolateApplyPR(Transform[] targetTransforms)
        {
            for (var i = 0; i < targetTransforms.Length; i++)
            {
                var src = currentBoneTransforms[i];
                var tgt = targetTransforms[i];
                tgt.localPosition = src.localPosition;
                tgt.localRotation = src.localRotation;
            }
        }
        
        /// <summary>
        ///     Apply last transform without interpolation
        /// </summary>
        /// <param name="targetTransforms"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void InterpolateApplyR(Transform[] targetTransforms)
        {
            for (var i = 0; i < targetTransforms.Length; i++)
                targetTransforms[i].localRotation = currentBoneTransforms[i].localRotation;
        }
        
        // =============================================================================================================
        // Set position rotation and parent
        // =============================================================================================================

        /// <summary>
        ///     Set parent transform of this entity
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="worldPositionStays"></param>
        public void SetParent(Transform parent, bool worldPositionStays = false)
        {
            transform.SetParent(parent, worldPositionStays);
            if (worldPositionStays)
                return;
            ForgetPreviousTransforms();
        }

        /// <summary>
        ///     Set position of entity
        /// </summary>
        /// <param name="position"></param>
        public void SetPosition(Vector3 position)
        {
            rigidbody.position = position;            
            ForgetPreviousTransforms();
        }
        
        /// <summary>
        ///     Set rotation of entity
        /// </summary>
        /// <param name="rotation"></param>
        public void SetRotation(Quaternion rotation)
        {
            rigidbody.rotation = rotation;            
            ForgetPreviousTransforms();
        }
        
        
        /// <summary>
        ///     Set position and rotation of entty
        /// </summary>
        /// <param name="position"></param>
        /// <param name="rotation"></param>
        public void SetPositionRotation(Vector3 position, Quaternion rotation)
        {
            rigidbody.position = position;  
            rigidbody.rotation = rotation;    
            ForgetPreviousTransforms();
        }
    }
}