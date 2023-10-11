using UnityEngine;

namespace Systemics
{
    public abstract class Variable<T> : ScriptableObject, 
        ISerializationCallbackReceiver
    {
        [SerializeField] private T value;

        private T _runtimeValue;

        public T Value
        {
            get => _runtimeValue;
            set => _runtimeValue = value;
        }

        public virtual void OnBeforeSerialize()
        {
            _runtimeValue = value;
        }
        
        public virtual void OnAfterDeserialize() {}
    }
}