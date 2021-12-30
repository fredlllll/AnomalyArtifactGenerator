namespace AnomalyDynamicArtifactsGenerator
{
    public interface  IArtifactPropertyStats
    {
        ArtifactProperty ArtifactProperty { get; }
        int Steps { get; }
        float GetValue(int index);
    }
}
