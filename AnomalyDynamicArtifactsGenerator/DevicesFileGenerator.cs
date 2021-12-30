using System.Collections.Generic;
using System.IO;

namespace AnomalyDynamicArtifactsGenerator
{
    public class DevicesFileGenerator
    {
        private readonly string target;
        private StreamWriter targetWriter;

        public DevicesFileGenerator(string target = "items_devices.ltx")
        {
            Directory.CreateDirectory(Path.GetDirectoryName(target));

            this.target = target;
        }

        private void WriteArtifact(string name, int index)
        {
            targetWriter.WriteLine($"af_class_{index} = {name}");
            targetWriter.WriteLine($"af_sound_{index}_ = detectors\\art_beep1");
            targetWriter.WriteLine($"af_freq_{index} = 0.05,2");
        }

        public void Write(IEnumerable<Artifact> artifacts)
        {
            var rank1 = new List<string>()
            {
                "af_itcher",
                "af_pin",
                "af_blood",
                "af_mincer_meat",
                "af_electra_sparkler",
                "af_sponge",
                "af_cristall_flower",
                "af_lobster_eyes",
                "af_medusa",
                "af_night_star",
                "af_dummy_glassbeads",
                "af_dummy_battery",
                "af_soul",
            };
            var rank2 = new List<string>()
            {
                "af_cristall",
                "af_bracelet",
                "af_ring",
                "af_electra_moonlight",
                "af_vyvert",
                "af_empty",
                "af_gravi",
                "af_eye",
                "af_dummy_dummy",
                "af_fuzz_kolobok",
            };
            var rank3 = new List<string>()
            {
                "af_fireball",
                "af_baloon",
                "af_electra_flash",
                "af_black_spray",
                "af_full_empty",
                "af_gold_fish",
                "af_fire",
                "af_ice",
                "af_glass",
                "af_death_lamp",
            };

            Dictionary<int, List<string>> byRank = new Dictionary<int, List<string>>()
            {
                { 1, rank1 },
                { 2, rank2 },
                { 3, rank3 },
            };

            foreach (var a in artifacts)
            {
                var l = byRank[a.Rank];
                l.Add("af_" + a.ID);
            }

            int index = 36;

            using (var fs = new FileStream(target, FileMode.Create, FileAccess.Write))
            using (targetWriter = new StreamWriter(fs))
            {
                targetWriter.WriteLine(File.ReadAllText("templates/items_devices_1.ltx"));
                //rank1
                foreach (var a in rank1)
                {
                    WriteArtifact(a, index);
                    index++;
                }
                targetWriter.WriteLine(File.ReadAllText("templates/items_devices_2.ltx"));
                //rank2
                foreach (var a in rank2)
                {
                    WriteArtifact(a, index);
                    index++;
                }
                targetWriter.WriteLine(File.ReadAllText("templates/items_devices_3.ltx"));
                //rank3
                foreach (var a in rank3)
                {
                    WriteArtifact(a, index);
                    index++;
                }
                targetWriter.WriteLine(File.ReadAllText("templates/items_devices_4.ltx"));
            }
        }
    }
}
