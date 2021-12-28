using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AnomalyDynamicArtifactsGenerator
{
    public class ArtifactsFileGenerator
    {
        string target;
        StreamWriter targetWriter;

        public ArtifactsFileGenerator(string target = "artefacts.ltx")
        {
            Directory.CreateDirectory(Path.GetDirectoryName(target));

            this.target = target;
        }

        public void Write(IEnumerable<Artifact> artifacts)
        {
            var gravi = new List<string>() {
                "af_black_spray",
                "af_night_star",
                "af_gravi",
                "af_gold_fish",
                "af_medusa",
                "af_vyvert",
                "af_empty",
                "af_full_empty",
                "af_death_lamp",
            };

            var thermo = new List<string>()
            {
                "af_itcher",
                "af_pin",
                "af_cristall",
                "af_fireball",
                "af_dummy_glassbeads",
                "af_eye",
                "af_fire",
                "af_lobster_eyes",
            };

            var chem = new List<string>()
            {
                "af_blood",
                "af_mincer_meat",
                "af_bracelet",
                "af_baloon",
                "af_soul",
                "af_fuzz_kolobok",
                "af_glass",
                "af_cristall_flower",
            };

            var electro = new List<string>()
            {
                "af_electra_sparkler",
                "af_sponge",
                "af_ring",
                "af_electra_flash",
                "af_dummy_battery",
                "af_dummy_dummy",
                "af_ice",
                "af_electra_moonlight",
            };

            Dictionary<ArtifactType, List<string>> namesByType = new Dictionary<ArtifactType, List<string>>() {
                {ArtifactType.Gravi,gravi },
                {ArtifactType.Thermo,thermo},
                {ArtifactType.Chem,chem },
                {ArtifactType.Electro,electro },
            };

            foreach (var a in artifacts)
            {
                var l = namesByType[a.ArtifactType];
                l.Add("af_" + a.ID);
            }

            using (var fs = new FileStream(target, FileMode.Create, FileAccess.Write))
            using (targetWriter = new StreamWriter(fs))
            {
                targetWriter.WriteLine(@"[settings]
psi_storm_chance = 0.7
surge_chance = 1.4

[artefact_groups]
af_class_gravi
af_class_thermo
af_class_chem
af_class_electro
af_class_gravi_musor
af_class_thermo_musor
af_class_chem_musor
af_class_electro_musor

[af_class_gravi]");

                foreach (var n in namesByType[ArtifactType.Gravi])
                {
                    targetWriter.WriteLine(n);
                }
                targetWriter.WriteLine();
                targetWriter.WriteLine("[af_class_thermo]");

                foreach (var n in namesByType[ArtifactType.Thermo])
                {
                    targetWriter.WriteLine(n);
                }
                targetWriter.WriteLine();
                targetWriter.WriteLine("[af_class_chem]");

                foreach (var n in namesByType[ArtifactType.Chem])
                {
                    targetWriter.WriteLine(n);
                }
                targetWriter.WriteLine();
                targetWriter.WriteLine("[af_class_electro]");

                foreach (var n in namesByType[ArtifactType.Electro])
                {
                    targetWriter.WriteLine(n);
                }
                targetWriter.WriteLine();

                targetWriter.WriteLine(@"[af_class_gravi_musor]
af_ear
af_signet
af_fountain
af_lighthouse
af_cell
af_sun
af_chelust

[af_class_thermo_musor]
af_repei
af_zhelch
af_cocoon
af_fire_loop
af_spaika
af_sandstone
af_dragon_eye

[af_class_chem_musor]
af_skull_miser
af_peas
af_black_angel
af_medallion
af_star_phantom
af_grapes
af_kislushka

[af_class_electro_musor]
af_ball
af_moh
af_fonar
af_tapeworm
af_generator
af_elektron
af_serofim
af_kogot");
            }
        }
    }
}
