using UnityEngine;

namespace Utility
{
    public class Functions
    {
        /// <summary>
        /// Returns a spaced vector array arranged in a circle. Useful for many different cases, like ContextMap generation.
        /// </summary>
        /// <param name="resolution">The number of points to generate.</param>
        /// <param name="startVector">The starting vector direction.</param>
        /// <returns></returns>
        public static Vector3[] RotatedVectorArray(int resolution, Vector3 startVector)
        {
            var returnArray = new Vector3[resolution];

            for(int i = 0; i < resolution; ++i)
            {
                var angle = ((i * Mathf.PI * 2 / (resolution))) * Mathf.Rad2Deg;
                returnArray[i] = (Quaternion.AngleAxis(angle, Vector3.up) * startVector);
                returnArray[i] = returnArray[i].normalized;
            }

            return returnArray;
        }

        public static Color[] ColorArray = new Color[]
        {
            Color.red,
            Color.green,
            Color.blue,
            Color.cyan,
            Color.magenta,
            Color.yellow,
            Color.white,
            Color.black,
            Color.white + Color.black,
            Color.red + Color.green
        };

        public static float ClampAngle(float angle, float min, float max)
        {
            float start = (min + max) * 0.5f - 180;
            float floor = Mathf.FloorToInt((angle - start) / 360) * 360;
            return Mathf.Clamp(angle, min + floor, max + floor);
        }
    }
}