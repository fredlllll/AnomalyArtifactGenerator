using System;
using System.Collections.Generic;
using System.Text;

namespace AnomalyDynamicArtifactsGenerator
{
    public class Counter
    {
        private readonly int[] counters;
        public readonly int[] maxs;
        public Counter(int size)
        {
            counters = new int[size];
            maxs = new int[size];
            for (int i = 0; i < size; ++i)
            {
                counters[i] = 0;
            }
        }

        public bool Increment()
        {
            for (int i = 0; i < counters.Length; ++i)
            {
                counters[i]++;
                if (counters[i] <= maxs[i])
                {
                    return true;
                }
                else
                {
                    counters[i] = 0;
                }
            }
            return false;
        }

        public int this[int index]
        {
            get { return counters[index]; }
        }
    }
}
