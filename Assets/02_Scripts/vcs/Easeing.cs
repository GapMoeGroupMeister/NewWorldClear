using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EaseFunc
{
    public class Easeing
    {
        /**
         * <summary>
         * x값이 증가 할 수록 약하게 y증가폭이 증가함
         * </summary>
         */
        public static float EaseInSine(float x) 
        {
            return 1 - Mathf.Cos((x * Mathf.PI) / 2);
        }
        
        /**
         * <summary>
         * 한 번에 y값이 대폭 증가했다가 천천히 늘어남
         * </summary>
         */
        public static float EaseOutExpo(float x)
        {
            return x == 1 ? 1 : 1 - Mathf.Pow(2, -10 * x);
        }
    }
}
