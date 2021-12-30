namespace AnomalyDynamicArtifactsGenerator
{
    public class ArtifactPropertyStatsList : IArtifactPropertyStats
    {
        private readonly float[] values;

        public ArtifactProperty ArtifactProperty
        {
            get;
        }

        public int Steps
        {
            get
            {
                return values.Length;
            }
        }

        public float GetValue(int index)
        {
            return values[index];
        }

        public ArtifactPropertyStatsList(ArtifactProperty artifactProperty, float[] values)
        {
            ArtifactProperty = artifactProperty;
            this.values = values;
        }
    }
}
