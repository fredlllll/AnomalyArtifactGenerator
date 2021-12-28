using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace AnomalyDynamicArtifactsGenerator
{
    public class Artifact : IEquatable<Artifact>
    {
        private static int idCounter = 100000;

        public int Rank
        {
            get;
        }

        private static readonly ArtifactType[] artifactTypes = {
            ArtifactType.Gravi,
            ArtifactType.Thermo,
            ArtifactType.Chem,
            ArtifactType.Electro
        };

        public ArtifactType ArtifactType
        {
            get;
        }

        public string Name
        {
            get
            {
                string name = "genArt_";
                foreach (var kv in properties)
                {
                    var propName = kv.Key.ToString();
                    name += propName.Substring(0, 2) + propName.Substring(propName.Length - 2, 2) + kv.Value.ToString("F8", System.Globalization.CultureInfo.InvariantCulture).TrimEnd('0').TrimEnd('.');
                }
                return name;
            }
        }

        private int id = 0;
        public string ID
        {
            get
            {
                if (id == 0)
                {
                    id = idCounter++;
                }
                return id.ToString();
            }
        }

        public int Cost
        {
            get
            {
                return 1337; //TODO: calculate from properties?
            }
        }

        public float Weight
        {
            get
            {
                return 1; //TODO: calc?
            }
        }

        public readonly Dictionary<ArtifactProperty, float> properties = new Dictionary<ArtifactProperty, float>();

        public Artifact()
        {
            //lets just use a random one for now
            ArtifactType = artifactTypes[Util.r.Next(artifactTypes.Length)];
            //also random till i really need it
            Rank = Util.r.Next(3) + 1; //1-3
        }

        public override bool Equals(object obj)
        {
            return obj is Artifact artifact &&
                   EqualityComparer<Dictionary<ArtifactProperty, float>>.Default.Equals(properties, artifact.properties);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(properties);
        }

        public bool Equals([AllowNull] Artifact other)
        {
            if (this == other)
            {
                return true;
            }

            foreach (var kv in properties)
            {
                if (other.properties.TryGetValue(kv.Key, out float otherValue))
                {
                    if (kv.Value != otherValue)
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        public override string ToString()
        {
            string retval = "";

            foreach (var kv in properties)
            {
                retval += kv.Key + ": " + kv.Value + "; ";
            }

            return retval;
        }
    }
}
