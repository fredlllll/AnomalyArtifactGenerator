using System;
using System.Collections.Generic;
using System.Text;

namespace AnomalyDynamicArtifactsGenerator
{
    public static class StringDb
    {
        public static readonly Dictionary<string, string> strings = new Dictionary<string, string>();
        public static string GetString(string value)
        {
            if (!strings.TryGetValue(value, out string id))
            {
                id = "dynart_" + strings.Count;
                strings[value] = id;
            }
            return id;
        }
    }
}
