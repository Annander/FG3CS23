using UnityEngine;

namespace Systemics.Variables
{
    [CreateAssetMenu(fileName = "New Curve Variable", menuName = "Variables/Curve")]
    public class CurveVariable : Variable<AnimationCurve> {}
}