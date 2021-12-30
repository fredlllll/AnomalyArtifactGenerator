using System;
using System.Collections.Generic;
using System.Text;

namespace AnomalyDynamicArtifactsGenerator
{
    public class ArtifactPropertyStats : IArtifactPropertyStats
    {
        public ArtifactProperty ArtifactProperty { get; }
        public int Steps { get; }
        public readonly float min, max;
        private readonly float diff;

        public ArtifactPropertyStats(ArtifactProperty artifactProperty, float min, float max, int steps)
        {
            ArtifactProperty = artifactProperty;
            this.min = min;
            this.max = max;
            Steps = steps;

            diff = (max - min) / (steps - 1);
        }

        public float GetValue(int index)
        {
            if (index < 0 || index >= this.Steps)
            {
                throw new IndexOutOfRangeException("index is out of range, has to be [0," + (Steps - 1) + "]");
            }
            return this.min + diff * index;
        }
    }
}
