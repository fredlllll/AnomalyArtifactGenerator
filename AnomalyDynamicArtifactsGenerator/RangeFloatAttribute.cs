using System;
using System.Collections.Generic;
using System.Text;

namespace AnomalyDynamicArtifactsGenerator
{
    public class RangeFloatAttribute : System.Attribute
    {
        public readonly float min, max;
        public readonly int steps;
        public RangeFloatAttribute(float min, float max, int steps = 5)
        {
            this.min = min;
            this.max = max;
            this.steps = steps;
        }
    }
}
