using UnityEngine;

namespace Utility
{
    public class PositionFollower : MonoBehaviour
    {
        [SerializeField]
        private Transform targetTransform;

        [SerializeField]
        private Vector3 offset;

        private void LateUpdate () 
        {
            transform.position = targetTransform.TransformPoint(offset);
        }

#if UNITY_EDITOR
        private void OnDrawGizmos() {
            if (targetTransform != null)
                DebugUtility.DrawCross(targetTransform.TransformPoint(offset), Color.cyan,.1f);
        }
#endif
    }
}