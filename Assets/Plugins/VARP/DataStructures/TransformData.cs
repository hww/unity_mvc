using UnityEngine;

namespace VARP.DataStructures
{
    /// <summary>The transform state</summary>
    public struct TransformData 
    {
        public Vector3 localPosition;
        public Quaternion localRotation;
        public Vector3 localScale;

        public TransformData(Vector3 localPosition, Quaternion localRotation, Vector3 localScale) {
            this.localPosition = localPosition;
            this.localRotation = localRotation;
            this.localScale = localScale;
        }

        public void Set(Transform transform)
        {
            localPosition = transform.localPosition;
            localScale = transform.localScale;
            localRotation = transform.localRotation;
        }

        public void Get(Transform transform)
        {
            transform.localPosition = localPosition;
            transform.localScale = localScale;
            transform.localRotation = localRotation;
        }
    }
}