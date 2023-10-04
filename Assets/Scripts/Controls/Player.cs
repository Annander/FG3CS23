using UnityEngine;

namespace Controls
{
    public class Player : MonoBehaviour
    {
        public Transform Root => _rootTransform;
        public Transform Head => headTransform;
        public Transform Noise => noiseTransform;
        public Transform Camera => _cameraTransform;
        
        [SerializeField] protected Transform headTransform;
        [SerializeField] protected Transform noiseTransform;
        
        private Transform _rootTransform;
        private Transform _cameraTransform;

        protected virtual void Awake()
        {
            _rootTransform = transform;
            _cameraTransform = GetComponentInChildren<UnityEngine.Camera>().transform;
        }
    }
}