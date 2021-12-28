using System;
using System.Collections.Generic;
using System.Text;

namespace AnomalyDynamicArtifactsGenerator
{
    public class ArtifactPropertyStats
    {
        public readonly ArtifactProperty artifactProperty;
        public readonly float min, max;
        public readonly int steps;
        private float diff;

        public ArtifactPropertyStats(ArtifactProperty artifactProperty, float min, float max, int steps)
        {
            this.artifactProperty = artifactProperty;
            this.min = min;
            this.max = max;
            this.steps = steps;

            diff = (max - min) / (steps-1);
        }

        public float GetValue(int index)
        {
            if (index < 0 || index >= this.steps)
            {
                throw new IndexOutOfRangeException("index is out of range, has to be [0," + (steps - 1) + "]");
            }
            return this.min + diff * index;
        }
    }
}
