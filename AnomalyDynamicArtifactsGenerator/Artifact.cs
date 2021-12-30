using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace AnomalyDynamicArtifactsGenerator
{
    public class Artifact : IEquatable<Artifact>
    {
        private static int idCounter = Util.r.Next(56789, 156789);

        private static readonly ArtifactType[] artifactTypes = {
            ArtifactType.Gravi,
            ArtifactType.Thermo,
            ArtifactType.Chem,
            ArtifactType.Electro
        };

        public int Rank
        {
            get;
        }

        ArtifactType artifactType = ArtifactType.None;
        public ArtifactType ArtifactType
        {
            get
            {
                if (artifactType == ArtifactType.None)
                {
                    //the following gravitates towards gravi and thermo, so i stay with random cause it doesnt make a difference
                    /*if (properties.ContainsKey(ArtifactProperty.AdditionalInventoryCarryWeight))
                    {
                        artifactType = ArtifactType.Gravi;
                    }
                    else if (properties.ContainsKey(ArtifactProperty.BurnImmunity) || properties.ContainsKey(ArtifactProperty.FireWoundImmunity))
                    {
                        artifactType = ArtifactType.Thermo;
                    }
                    else if (properties.ContainsKey(ArtifactProperty.ChemicalBurnImmunity))
                    {
                        artifactType = ArtifactType.Chem;
                    }
                    else if (properties.ContainsKey(ArtifactProperty.ShockImmunity))
                    {
                        artifactType = ArtifactType.Electro;
                    }
                    else
                    {
                        artifactType = artifactTypes[Util.r.Next(artifactTypes.Length)];
                    }*/

                    artifactType = artifactTypes[Util.r.Next(artifactTypes.Length)];
                }
                return artifactType;
            }
        }

        public string NameId
        {
            get
            {
                return StringDb.GetString(Name);
            }
        }

        public string Name
        {
            get
            {
                return "Artifact " + ID;

                //debug name:
                /*string name = "genArt_";
                foreach (var kv in properties)
                {
                    var propName = kv.Key.ToString();
                    name += propName.Substring(0, 2) + propName.Substring(propName.Length - 2, 2) + kv.Value.ToString("F8", System.Globalization.CultureInfo.InvariantCulture).TrimEnd('0').TrimEnd('.');
                }
                return name;*/
            }
        }

        private int id = 0;
        public string ID
        {
            get
            {
                if (id == 0)
                {
                    id = idCounter;
                    idCounter += Util.r.Next(333, 999);
                }
                return id.ToString();
            }
        }


        int cost = -1;
        public int Cost
        {
            get
            {
                if (cost < 0)
                {
                    switch (Rank)
                    {
                        case 1:
                            cost = Util.r.Next(7000, 17000);
                            break;
                        case 2:
                            cost = Util.r.Next(17000, 37000);
                            break;
                        case 3:
                            cost = Util.r.Next(37000, 77000);
                            break;
                    }
                }
                return cost;
            }
        }

        public float Weight
        {
            get;
        }

        private float radiationRestoreSpeed = float.NaN;
        public float RadiationRestoreSpeed
        {
            get
            {
                if (!float.IsNaN(radiationRestoreSpeed))
                {
                    return radiationRestoreSpeed;
                }
                switch (Rank)
                {
                    case 1:
                        return 0.00013f;
                    case 2:
                        return 0.00025f;
                    case 3:
                        return 0.00047f;
                }
                return 0;
            }
            set
            {
                radiationRestoreSpeed = value;
            }
        }

        public readonly Dictionary<ArtifactProperty, float> properties = new Dictionary<ArtifactProperty, float>();

        public Graphics Graphics { get; }

        public Artifact()
        {
            //also random till i really need it
            Rank = Util.r.Next(3) + 1; //1-3
            //random
            Weight = 1.33f + (float)(Util.r.NextDouble() * 3);
            Graphics = GraphicsDb.GetRandom();
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
