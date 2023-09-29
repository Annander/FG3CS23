using System.Collections.Generic;
using UnityEngine;

namespace Systemics
{
    public class DoubleParameterEvent<T, TT> : ScriptableObject
    {
        private readonly List<DoubleParameterListener<T, TT>> _listeners = new List<DoubleParameterListener<T, TT>>();

        public void Raise(T a, TT b)
        {
            for (int i = _listeners.Count - 1; i >= 0; i--)
                _listeners[i].OnEventRaised(a, b);
        }

        public void RegisterListener(DoubleParameterListener<T, TT> listener)
        {
            _listeners.Add(listener);
        }

        public void UnregisterListener(DoubleParameterListener<T, TT> listener)
        {
            _listeners.Remove(listener);
        }
    }
}