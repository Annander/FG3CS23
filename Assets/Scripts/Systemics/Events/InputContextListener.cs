using UnityEngine.InputSystem;

namespace Systemics.Events
{
    public class InputContextListener : SingleParameterListener<InputAction.CallbackContext>
    {
        public InputContextListener(InputContextEvent contextEvent) : base(contextEvent) 
        {}
    }
}