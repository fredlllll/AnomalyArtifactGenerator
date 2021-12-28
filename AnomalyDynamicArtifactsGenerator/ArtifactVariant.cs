using System;
using System.Collections.Generic;
using System.Text;

namespace AnomalyDynamicArtifactsGenerator
{
    public class ArtifactVariant
    {
        public readonly List<ArtifactPropertyStats> properties = new List<ArtifactPropertyStats>();

        public IEnumerable<Artifact> GetArtifacts()
        {
            List<Artifact> artifacts = new List<Artifact>();

            int totalCount = 1;
            foreach (var p in properties)
            {
                totalCount *= p.steps;
            }

            Counter counter = new Counter(properties.Count);
            for (int i = 0; i < properties.Count; ++i)
            {
                counter.maxs[i] = properties[i].steps - 1;
            }

            do
            {
                Artifact a = new Artifact();
                for (int i = 0; i < properties.Count; ++i)
                {
                    var p = properties[i];
                    var val = p.GetValue(counter[i]);
                    if (val == 0 || val.NearlyEqual(0))
                    {
                        //skip artifacts with 0 values for properties
                        a = null;
                        break;
                    }
                    a.properties[p.artifactProperty] = val;
                }
                if (a != null)
                {
                    artifacts.Add(a);
                }
            } while (counter.Increment());

            return artifacts;
        }
    }
}
