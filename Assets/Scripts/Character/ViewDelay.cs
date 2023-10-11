using UnityEngine;

namespace Character
{
    public class ViewDelay : MonoBehaviour
    {
        [SerializeField]
        private Transform targetTransform;

        [SerializeField]
        private float multiplier;

        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
        }

        private void Update()
        {
            _transform.position = targetTransform.position;
            _transform.rotation = Quaternion.Lerp(_transform.rotation, targetTransform.rotation, multiplier * Time.deltaTime);
        }
    }
}