using System;
using System.Collections.Generic;
using System.IO;

namespace AnomalyDynamicArtifactsGenerator
{
    public class ArtifactGenerator
    {
        private readonly List<IArtifactPropertyStats> properties = new List<IArtifactPropertyStats>();

        public ArtifactGenerator(string propertyStatsFile)
        {
            using (var fs = new FileStream(propertyStatsFile, FileMode.Open, FileAccess.Read))
            using (var sr = new StreamReader(fs))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    line = line.Trim();
                    if (line.Length == 0 || line.StartsWith("#"))
                    {
                        continue;
                    }

                    var parts = line.Split(',');

                    IArtifactPropertyStats aps;
                    var name = parts[0];
                    if (name.EndsWith("[]"))
                    {
                        name = name.Substring(0, name.Length - 2);

                        List<float> vals = new List<float>();
                        for (int i = 1; i < parts.Length; ++i)
                        {
                            vals.Add(float.Parse(parts[i], System.Globalization.CultureInfo.InvariantCulture));
                        }

                        aps = new ArtifactPropertyStatsList((ArtifactProperty)Enum.Parse(typeof(ArtifactProperty), name), vals.ToArray());
                    }
                    else
                    {
                        aps = new ArtifactPropertyStats((ArtifactProperty)Enum.Parse(typeof(ArtifactProperty), name),
                        float.Parse(parts[1], System.Globalization.CultureInfo.InvariantCulture),
                        float.Parse(parts[2], System.Globalization.CultureInfo.InvariantCulture),
                        int.Parse(parts[3])
                        );
                    }
                    properties.Add(aps);
                }
            }
        }

        public IEnumerable<Artifact> GetArtifacts(int props = 3)
        {
            var propList = NOverK.Combinations(properties, props);

            foreach (var properties in propList)
            {
                var av = new ArtifactVariant();

                foreach (var p in properties)
                {
                    av.properties.Add(p);
                }

                foreach (var a in av.GetArtifacts())
                {
                    yield return a;
                }
            }
        }
    }
}
