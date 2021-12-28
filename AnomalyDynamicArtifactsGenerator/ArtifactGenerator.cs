using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AnomalyDynamicArtifactsGenerator
{
    public class ArtifactGenerator
    {
        private readonly List<ArtifactPropertyStats> properties = new List<ArtifactPropertyStats>();

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

                    if (parts.Length != 4)
                    {
                        Console.WriteLine("cant parse this shit: " + line);
                        continue;
                    }

                    ArtifactPropertyStats aps = new ArtifactPropertyStats((ArtifactProperty)Enum.Parse(typeof(ArtifactProperty), parts[0]),
                        float.Parse(parts[1], System.Globalization.CultureInfo.InvariantCulture),
                        float.Parse(parts[2], System.Globalization.CultureInfo.InvariantCulture),
                        int.Parse(parts[3])
                        );
                    properties.Add(aps);
                }
            }
        }

        public IEnumerable<Artifact> GetArtifacts(int props = 3)
        {
            List<Artifact> artifacts = new List<Artifact>();

            var propList = NOverK.Combinations(properties, props);

            foreach (var properties in propList)
            {
                var av = new ArtifactVariant();

                foreach (var p in properties)
                {
                    av.properties.Add(p);
                }

                artifacts.AddRange(av.GetArtifacts());
            }

            return artifacts;
        }
    }
}
