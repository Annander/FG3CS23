using UnityEngine;

namespace Utility.Easing
{
    public class Functions
    {
        public static float GetEaseValue(EasingType easingType, float x)
        {
            switch(easingType) 
            {
                case EasingType.SineIn: return Sine_In(x);
                case EasingType.SineOut: return Sine_Out(x);
                case EasingType.SineInOut: return Sine_InOut(x);
                case EasingType.QuadIn: return Quad_In(x);
                case EasingType.QuadOut: return Quad_Out(x);
                case EasingType.QuadInOut: return Quad_InOut(x);
                case EasingType.CubeIn: return Cubic_In(x);
                case EasingType.CubeOut: return Cubic_Out(x);
                case EasingType.CubeInOut: return Cubic_InOut(x);
                case EasingType.QuartIn: return Quart_In(x);
                case EasingType.QuartOut: return Quart_Out(x);
                case EasingType.QuartInOut: return Quart_InOut(x);
                case EasingType.QuintIn: return Quint_In(x);
                case EasingType.QuintOut: return Quint_Out(x);
                case EasingType.QuintInOut: return Quint_InOut(x);
                case EasingType.ExponentialIn: return Expo_In(x);
                case EasingType.ExponentialOut: return Expo_Out(x);
                case EasingType.ExponentialInOut: return Expo_InOut(x);
                case EasingType.CircularIn: return Circ_In(x);
                case EasingType.CircularOut: return Circ_Out(x);
                case EasingType.CircularInOut: return Circ_InOut(x);
                case EasingType.BackBounceIn: return Back_In(x);
                case EasingType.BackBounceOut: return Back_Out(x);
                case EasingType.BackBounceInOut: return Back_InOut(x);
                case EasingType.ElasticIn: return Elastic_In(x);
                case EasingType.ElasticOut: return Elastic_Out(x);
                case EasingType.ElasticInOut: return Elastic_InOut(x);
                case EasingType.BounceIn: return Bounce_In(x);
                case EasingType.BounceOut: return Bounce_Out(x);
                case EasingType.BounceInOut: return Bounce_InOut(x);
                default: return x;
            }
        }        
        
        private static float Sine_In(float x)
        {
            return 1f - Mathf.Cos((x * Mathf.PI) / 2);
        }

        private static float Sine_Out(float x)
        {
            return Mathf.Sin((x * Mathf.PI) / 2);
        }

        private static float Sine_InOut(float x)
        {
            return -(Mathf.Cos(Mathf.PI * x) - 1) / 2;
        }

        private static float Quad_In(float x)
        {
            return x * x;
        }

        private static float Quad_Out(float x)
        {
            return 1 - (1 - x) * (1 - x);
        }

        private static float Quad_InOut(float x)
        {
            return x < .5f ? 2 * x * x : 1 - Mathf.Pow(-2 * x + 2, 2) / 2;
        }

        private static float Cubic_In(float x)
        {
            return x * x * x;
        }

        private static float Cubic_Out(float x)
        {
            return 1 - Mathf.Pow(1 - x, 3);
        }

        private static float Cubic_InOut(float x)
        {
            return x < .5f ? 4 * x * x * x : 1 - Mathf.Pow(-2 * x + 2, 3) / 2;
        }

        private static float Quart_In(float x)
        {
            return x * x * x * x;
        }

        private static float Quart_Out(float x)
        {
            return 1 - Mathf.Pow(1 - x, 4);
        }

        private static float Quart_InOut(float x)
        {
            return x < .5f ? 8 * x * x * x * x : 1 - Mathf.Pow(-2 * x + 2, 4) / 2;
        }

        private static float Quint_In(float x)
        {
            return x * x * x * x * x;
        }

        private static float Quint_Out(float x)
        {
            return 1 - Mathf.Pow(1 - x, 5);
        }

        private static float Quint_InOut(float x)
        {
            return x < .5f ? 16 * x * x * x * x * x : 1 - Mathf.Pow(-2 * x + 2, 5) / 2;
        }

        private static float Expo_In(float x)
        {
            return Mathf.Approximately(x, 0f) ? 0 : Mathf.Pow(2, 10 * x - 10);
        }

        private static float Expo_Out(float x)
        {
            return Mathf.Approximately(x, 1f) ? 1 : 1 - Mathf.Pow(2, -10 * x);
        }

        private static float Expo_InOut(float x)
        {
            return Mathf.Approximately(x, 0f) ? 0 : Mathf.Approximately(x, 1f) ? 1 : x < 0.5 ? Mathf.Pow(2, 20 * x - 10) / 2 : (2 - Mathf.Pow(2, -20 * x + 10)) / 2;
        }

        private static float Circ_In(float x)
        {
            return 1 - Mathf.Sqrt(1 - Mathf.Pow(x, 2));
        }

        private static float Circ_Out(float x)
        {
            return Mathf.Sqrt(1 - Mathf.Pow(x - 1, 2));
        }

        private static float Circ_InOut(float x)
        {
            return x < .5f ? (1 - Mathf.Sqrt(1 - Mathf.Pow(2 * x, 2))) / 2 : (Mathf.Sqrt(1 - Mathf.Pow(-2 * x + 2, 2)) + 1) / 2;
        }

        private static float Back_In(float x)
        {
            const float c1 = 1.70158f;
            const float c3 = c1 + 1f;

            return c3 * x * x * x - c1 * x * x;
        }

        private static float Back_Out(float x)
        {
            const float c1 = 1.70158f;
            const float c3 = c1 + 1;

            return 1 + c3 * Mathf.Pow(x - 1, 3) + c1 * Mathf.Pow(x - 1, 2);
        }

        private static float Back_InOut(float x)
        {
            const float c1 = 1.70158f;
            const float c2 = c1 * 1.525f;

            return x < .5f ? (Mathf.Pow(2 * x, 2) * ((c2 + 1) * 2 * x - c2)) / 2 : (Mathf.Pow(2 * x - 2, 2) * ((c2 + 1) * (x * 2 - 2) + c2) + 2) / 2;
        }

        private static float Elastic_In(float x)
        {
            const float c4 = (2 * Mathf.PI) / 3;
            return Mathf.Approximately(x, 0f) ? 0 : Mathf.Approximately(x, 1f) ? 1 : -Mathf.Pow(2, 10 * x - 10) * Mathf.Sin((x * 10 - 10.75f) * c4);
        }

        private static float Elastic_Out(float x)
        {
            const float c4 = (2 * Mathf.PI) / 3;
            return Mathf.Approximately(x, 0f) ? 0 : Mathf.Approximately(x, 1f) ? 1 : Mathf.Pow(2, -10 * x) * Mathf.Sin((x * 10 - 0.75f) * c4) + 1;
        }

        private static float Elastic_InOut(float x)
        {
            const float c5 = (2 * Mathf.PI) / 4.5f;

            return Mathf.Approximately(x, 0f) ? 0 : Mathf.Approximately(x, 1f) ? 1 : x < 0.5f ? -(Mathf.Pow(2, 20 * x - 10) * Mathf.Sin((20 * x - 11.125f) * c5)) / 2
                : (Mathf.Pow(2, -20 * x + 10) * Mathf.Sin((20 * x - 11.125f) * c5)) / 2 + 1;
        }

        private static float Bounce_In(float x)
        {
            return 1 - Bounce_Out(1 - x);
        }

        private static float Bounce_Out(float x)
        {
            const float n1 = 7.5625f;
            const float d1 = 2.75f;

            if (x < 1 / d1)
            {
                return n1 * x * x;
            }
            else if (x < 2 / d1)
            {
                return n1 * (x -= 1.5f / d1) * x + 0.75f;
            }
            else if (x < 2.5 / d1)
            {
                return n1 * (x -= 2.25f / d1) * x + 0.9375f;
            }
            else
            {
                return n1 * (x -= 2.625f / d1) * x + 0.984375f;
            }
        }

        private static float Bounce_InOut(float x)
        {
            return x < .5f ? (1 - Bounce_Out(1 - 2 * x)) / 2 : (1 + Bounce_Out(2 * x - 1)) / 2;
        }
    }
}