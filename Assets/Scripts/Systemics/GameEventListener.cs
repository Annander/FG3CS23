namespace Systemics
{
    public delegate void GameEventTrigger();
    public delegate void SingleParameterEventTrigger<in T>(T a);
    public delegate void DoubleParameterEventTrigger<in T, in TT>(T a, TT b);

    [System.Serializable]
    public class GameEventListener
    {
        public event GameEventTrigger Response;
    
        private GameEvent _sourceEvent;

        public GameEventListener(GameEvent sourceEvent)
        {
            this._sourceEvent = sourceEvent;
            sourceEvent.RegisterListener(this);
        }

        ~GameEventListener()
        {
            _sourceEvent.UnregisterListener(this);
        }

        public void OnEventRaised()
        { 
            Response?.Invoke(); 
        }
    }

    [System.Serializable]
    public class SingleParameterListener<T>
    {
        public event SingleParameterEventTrigger<T> Response;

        private SingleParameterEvent<T> _sourceEvent;

        public SingleParameterListener(SingleParameterEvent<T> sourceEvent)
        {
            this._sourceEvent = sourceEvent;
            sourceEvent.RegisterListener(this);
        }

        ~SingleParameterListener()
        {
            _sourceEvent.UnregisterListener(this);
        }

        public void OnEventRaised(T param)
        {
            Response?.Invoke(param);
        }
    }

    [System.Serializable]
    public class DoubleParameterListener<T,TT>
    {
        public event DoubleParameterEventTrigger<T,TT> Response;

        private DoubleParameterEvent<T,TT> _sourceEvent;

        public DoubleParameterListener(DoubleParameterEvent<T, TT> sourceEvent)
        {
            this._sourceEvent = sourceEvent;
            sourceEvent.RegisterListener(this);
        }

        ~DoubleParameterListener()
        {
            _sourceEvent.UnregisterListener(this);
        }

        public void OnEventRaised(T a, TT b)
        {
            Response?.Invoke(a, b);
        }
    }
}