using System;
using System.Collections.Generic;
using System.Text;

namespace AnomalyDynamicArtifactsGenerator
{
    public class RangeIntAttribute : System.Attribute
    {
        public readonly int min, max;
        public readonly int steps;
        public RangeIntAttribute(int min, int max, int steps = 5)
        {
            this.min = min;
            this.max = max;
            this.steps = steps;
        }
    }
}
