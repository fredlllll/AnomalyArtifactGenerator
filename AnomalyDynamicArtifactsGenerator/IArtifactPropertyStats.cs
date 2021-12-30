using System;
using System.Collections.Generic;
using System.Text;

namespace AnomalyDynamicArtifactsGenerator
{
    public interface  IArtifactPropertyStats
    {
        ArtifactProperty ArtifactProperty { get; }
        int Steps { get; }
        float GetValue(int index);
    }
}
