using UnityEngine;
using Utility.Easing;

namespace Systemics.Variables
{
    [CreateAssetMenu(fileName = "New Curve Variable", menuName = "Variables/Easing")]
    public class EasingVariable : Variable<EasingType> {}
}