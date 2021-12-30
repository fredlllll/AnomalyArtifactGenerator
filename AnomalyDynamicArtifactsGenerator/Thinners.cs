using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AnomalyDynamicArtifactsGenerator
{
    public static class Thinners
    {
        public static IEnumerable<Artifact> ThinPercentage(IEnumerable<Artifact> input, float leftOverPercentage)
        {
            var count = input.LongCount();
            Console.WriteLine("Artifacts before thinning: " + count);
            count = (long)(count * leftOverPercentage);
            Console.WriteLine("Artifacts after thinning: " + count);
            return input.Shuffle().Take((int)count);
        }

        public static IEnumerable<Artifact> ThinFixed(IEnumerable<Artifact> input, int leftOver)
        {
            var count = input.LongCount();
            Console.WriteLine("Artifacts before thinning: " + count);
            Console.WriteLine("Artifacts after thinning: " + leftOver);
            return input.Shuffle().Take(leftOver);
        }

        public static IEnumerable<Artifact> ThinPercentageHealthRestore(IEnumerable<Artifact> input, float leftOverPercentage)
        {
            var healthRestore = input.Where(x => x.properties.ContainsKey(ArtifactProperty.HealthRestoreSpeed));

            var count = healthRestore.LongCount();
            Console.WriteLine("Health Restore Artifacts before thinning: " + count);
            long afterCount = (long)(count * leftOverPercentage);
            Console.WriteLine("Health Restore Artifacts after thinning: " + afterCount);
            count = count - afterCount;
            healthRestore = healthRestore.Shuffle().Take((int)count);
            return input.Except(healthRestore);
        }
    }
}
