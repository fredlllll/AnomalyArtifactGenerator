using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AnomalyDynamicArtifactsGenerator
{
    public class StringsFileGenerator
    {
        string target;
        StreamWriter targetWriter;

        public StringsFileGenerator(string target = "st_dynart.xml")
        {
            Directory.CreateDirectory(Path.GetDirectoryName(target));

            this.target = target;
        }

        void WriteString(string key, string value)
        {
            targetWriter.WriteLine($"<string id=\"{key}\">");
            targetWriter.WriteLine($"<text>{value}</text>");
            targetWriter.WriteLine("</string>");
        }

        public void Write()
        {
            using (var fs = new FileStream(target, FileMode.Create, FileAccess.Write))
            using (targetWriter = new StreamWriter(fs))
            {
                targetWriter.WriteLine("<?xml version=\"1.0\" encoding=\"windows-1251\"?><string_table>");

                foreach (var kv in StringDb.strings)
                {
                    WriteString(kv.Value, kv.Key);
                }

                targetWriter.WriteLine("</string_table>");
            }
        }
    }
}
