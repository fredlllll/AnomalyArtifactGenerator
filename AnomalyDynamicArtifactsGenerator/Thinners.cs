using System;
using System.Collections.Generic;
using System.Linq;

namespace AnomalyDynamicArtifactsGenerator
{
    public static class Thinners
    {
        public static IEnumerable<Artifact> ThinPercentage(IEnumerable<Artifact> input, float leftOverPercentage)
        {
            var count = input.LongCount();
            count = (long)(count * leftOverPercentage);
            return input.Shuffle().Take((int)count);
        }

        public static IEnumerable<Artifact> ThinFixed(IEnumerable<Artifact> input, int leftOver)
        {
            var count = input.LongCount();
            return input.Shuffle().Take(leftOver);
        }

        public static IEnumerable<Artifact> ThinPercentageHealthRestore(IEnumerable<Artifact> input, float leftOverPercentage)
        {
            var healthRestore = input.Where(x => x.properties.ContainsKey(ArtifactProperty.HealthRestoreSpeed));

            var count = healthRestore.LongCount();
            long afterCount = (long)(count * leftOverPercentage);
            count = count - afterCount;
            healthRestore = healthRestore.Shuffle().Take((int)count);
            return input.Except(healthRestore);
        }
    }
}
