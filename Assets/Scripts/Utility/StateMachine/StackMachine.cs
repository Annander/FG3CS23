using System.Collections.Generic;
using UnityEngine;

namespace Utility.StateMachine
{
	/// <summary>
	/// A stack-based state machine in very simple form.
	/// </summary>
	public sealed class StackMachine
	{
		public const string NOSTATE = "None";

		private readonly Stack<IState> _stack = new();

		public void Update()
		{
			if (_stack.Count > 0)
			{
				var state = _stack.Peek().OnUpdate();

				if (state == StateReturn.Completed)
					PopState();
			}
		}

		public void Clear() 
		{
			_stack.Clear();
		}

		public void PushState(IState state, bool onEnter = true)
		{
			if ( _stack.Count > 0 )
			{
				if ( _stack.Peek() == state )
					return;
				else
					_stack.Peek().OnExit();
			}

			if(onEnter && !state.HasEntered)
				state.OnEnter();

			_stack.Push(state);
		}

		public void PopState() 
		{
			if (_stack.Count == 0)
				return;

			_stack.Peek().OnExit();
			_stack.Pop();

			if(_stack.Count > 0)
			{
				var state = _stack.Peek();
            
				if(!state.HasEntered)
					state.OnEnter();
			}
		}

		public bool IsState(IState state)
		{
			if (_stack.Count < 1)
				return false;

			if( state == _stack.Peek() )
				return true;

			return false;
		}

#if UNITY_EDITOR
		public override string ToString()
		{
			return CurrentState;
		}

		public string CurrentState 
		{
			get {
				if (_stack.Count > 0)
					return _stack.Peek().ToString();

				return NOSTATE;
			}
		}

		public void OnDrawGizmosSelected(Vector3 position)
		{
			UnityEditor.Handles.Label(position, CurrentState);
		}
#endif
	}
}