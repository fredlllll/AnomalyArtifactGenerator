using System;
using System.Collections.Generic;
using System.Text;

namespace AnomalyDynamicArtifactsGenerator
{
    public class ArtifactVariant
    {
        public readonly List<IArtifactPropertyStats> properties = new List<IArtifactPropertyStats>();

        public IEnumerable<Artifact> GetArtifacts()
        {
            Counter counter = new Counter(properties.Count);
            for (int i = 0; i < properties.Count; ++i)
            {
                counter.maxs[i] = properties[i].Steps - 1;
            }

            do
            {
                Artifact a = new Artifact();
                for (int i = 0; i < properties.Count; ++i)
                {
                    var p = properties[i];
                    var val = p.GetValue(counter[i]);
                    /*if (val == 0 || val.NearlyEqual(0))
                    {
                        //skip artifacts with 0 values for properties
                        a = null;
                        break;
                    }*/
                    a.properties[p.ArtifactProperty] = val;
                }
                if (a != null)
                {
                    yield return a;
                }
            } while (counter.Increment());
        }
    }
}
