using UnityEngine;

namespace Utility
{
    [DefaultExecutionOrder(-1000)]
    public class UnitySingleton<T> : MonoBehaviour
        where T : MonoBehaviour
    {
        public static T Instance
        {
            get;
            private set;
        }

        protected virtual void Awake()
        {
            if (Instance)
                DestroyImmediate(gameObject);
            else
                Instance = this as T;
        }
    }
}