using UnityEngine;
using UnityEngine.InputSystem;

namespace Systemics.Events
{
    [CreateAssetMenu(fileName = "New InputContext Event", menuName = "Systemics/Events/InputContext Event")]
    public class InputContextEvent : SingleParameterEvent<InputAction.CallbackContext>
    {}
}