using System.Collections.Generic;
using UnityEngine;

namespace Systemics
{
    public class SingleParameterEvent<T> : ScriptableObject, ISerializationCallbackReceiver
    {
        private readonly List<SingleParameterListener<T>> _listeners = new List<SingleParameterListener<T>>();

        public void OnAfterDeserialize()
        {
            _listeners.Clear();
        }

        public void OnBeforeSerialize()
        {}

        public void Raise(T a)
        {
            for (int i = _listeners.Count - 1; i >= 0; i--)
                _listeners[i].OnEventRaised(a);
        }

        public void RegisterListener(SingleParameterListener<T> listener)
        {
            _listeners.Add(listener);
        }

        public void UnregisterListener(SingleParameterListener<T> listener)
        {
            _listeners.Remove(listener);
        }
    }
}