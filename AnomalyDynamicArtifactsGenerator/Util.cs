﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AnomalyDynamicArtifactsGenerator
{
    public static class Util
    {
        public static readonly Random r = new Random();

        public static bool NearlyEqual(this float a, float b, float epsilon = 0.0000001f)
        {
            if (a == b)
            { // shortcut, handles infinities
                return true;
            }

            float absA = Math.Abs(a);
            float absB = Math.Abs(b);
            float diff = Math.Abs(a - b);

            if (a == 0 || b == 0 || absA + absB < float.Epsilon)
            {
                // a or b is zero or both are extremely close to it
                // relative error is less meaningful here
                return diff < epsilon;
            }
            // use relative error
            return diff / (absA + absB) < epsilon;
        }
    }
}
