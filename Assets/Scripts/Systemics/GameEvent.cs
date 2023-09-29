using System.Collections.Generic;
using UnityEngine;

namespace Systemics
{
    [CreateAssetMenu(fileName = "New Game Event", menuName = "Systemics/Events/Game Event")]
    public class GameEvent : ScriptableObject
    {
        private readonly List<GameEventListener> _listeners = new List<GameEventListener>();

        public void Raise()
        {
            for (int i = _listeners.Count - 1; i >= 0; i--)
                _listeners[i].OnEventRaised();
        }

        public void RegisterListener(GameEventListener listener)
        {
            _listeners.Add(listener);
        }

        public void UnregisterListener(GameEventListener listener)
        {
            _listeners.Remove(listener);
        }
    }
}