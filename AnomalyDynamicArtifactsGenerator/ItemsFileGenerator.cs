using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AnomalyDynamicArtifactsGenerator
{
    public class ItemsFileGenerator
    {
        private static readonly ArtifactProperty[] immunities =
        {
            ArtifactProperty.BurnImmunity,
            ArtifactProperty.ChemicalBurnImmunity,
            ArtifactProperty.ExplosionImmunity,
            ArtifactProperty.FireWoundImmunity,
            ArtifactProperty.RadiationImmunity,
            ArtifactProperty.ShockImmunity,
            ArtifactProperty.StrikeImmunity,
            ArtifactProperty.TelepaticImmunity,
            ArtifactProperty.WoundImmunity,
        };

        private static readonly Dictionary<ArtifactProperty, string> immunityCodeNames = new Dictionary<ArtifactProperty, string>()
        {
            { ArtifactProperty.BurnImmunity,"burn_immunity" },
            { ArtifactProperty.ChemicalBurnImmunity,"chemical_burn_immunity" },
            { ArtifactProperty.ExplosionImmunity,"explosion_immunity" },
            { ArtifactProperty.FireWoundImmunity,"fire_wound_immunity" },
            { ArtifactProperty.RadiationImmunity,"radiation_immunity" },
            { ArtifactProperty.ShockImmunity,"shock_immunity" },
            { ArtifactProperty.StrikeImmunity,"strike_immunity" },
            { ArtifactProperty.TelepaticImmunity,"telepatic_immunity" },
            { ArtifactProperty.WoundImmunity,"wound_immunity" }
        };

        string target;
        StreamWriter targetWriter;

        public ItemsFileGenerator(string target = "items_dynart.ltx")
        {
            Directory.CreateDirectory(Path.GetDirectoryName(target));

            this.target = target;
        }

        string str(float val)
        {
            /*if (val == 0f)
            {
                return "0";
            }*/
            var ret = val.ToString("F8", System.Globalization.CultureInfo.InvariantCulture).TrimEnd('0').TrimEnd('.');
            if (!ret.Contains('.'))
            {
                ret = ret += ".0";
            }
            return ret;
        }

        void WriteArtifact(Artifact a)
        {
            //write artifact 
            string name = a.Name;
            string nameId = a.NameId;
            string id = a.ID;

            targetWriter.WriteLine("[af_" + id + "]:af_base");
            targetWriter.WriteLine("$spawn = " + a.Graphics.spawn); //no fuckin idea
            targetWriter.WriteLine("kind = i_arty");
            targetWriter.WriteLine("class = ARTEFACT");
            targetWriter.WriteLine("visual = " + a.Graphics.visual); //3d model
            targetWriter.WriteLine("inv_name = " + nameId);
            targetWriter.WriteLine("inv_name_short = " + nameId);
            targetWriter.WriteLine("description = " + StringDb.GetString("One of many artifacts that can be found in the zone"));
            targetWriter.WriteLine("inv_grid_x = "+a.Graphics.inv_x);
            targetWriter.WriteLine("inv_grid_y = "+a.Graphics.inv_y);
            targetWriter.WriteLine("cost = " + a.Cost);
            targetWriter.WriteLine("jump_height = 0"); //guess how much it bounces around when on ground?
            targetWriter.WriteLine("inv_weight = " + str(a.Weight));
            switch (a.ArtifactType)
            {
                case ArtifactType.Gravi:
                    targetWriter.WriteLine(@"particles = artefact\af_gravi_idle
det_show_particles = artefact\af_gravi_show
det_hide_particles = artefact\af_gravi_hide");
                    break;
                case ArtifactType.Thermo:
                    targetWriter.WriteLine(@"particles = artefact\af_thermal_idle
det_show_particles = artefact\af_thermal_show
det_hide_particles = artefact\af_thermal_hide");
                    break;
                case ArtifactType.Chem:
                    targetWriter.WriteLine(@"particles = artefact\af_acidic_idle
det_show_particles = artefact\af_acidic_show
det_hide_particles = artefact\af_acidic_hide");
                    break;
                case ArtifactType.Electro:
                    targetWriter.WriteLine(@"particles = artefact\af_electra_idle
det_show_particles = artefact\af_electra_show
det_hide_particles = artefact\af_electra_hide");
                    break;
            }
            targetWriter.WriteLine("af_rank=" + a.Rank);
            targetWriter.WriteLine("tier=" + (a.Rank + 1)); //tier always seems to be 1 higher, idk why there are rank and tier
            //random light
            targetWriter.WriteLine(@"lights_enabled = true
trail_light_color = " + str((float)Util.r.NextDouble()) + "," + str((float)Util.r.NextDouble()) + "," + str((float)Util.r.NextDouble()) + @"
trail_light_range = 2.0");
            targetWriter.WriteLine("hit_absorbation_sect = af_" + id + "_absorbation");
            targetWriter.WriteLine("radiation_restore_speed = " + str(a.RadiationRestoreSpeed));

            float value;
            if (a.properties.TryGetValue(ArtifactProperty.AdditionalInventoryCarryWeight, out value))
            {
                targetWriter.WriteLine("additional_inventory_weight	= " + str(value));
                targetWriter.WriteLine("additional_inventory_weight2 = " + str(value));
            }
            if (a.properties.TryGetValue(ArtifactProperty.BleedingRestoreSpeed, out value))
            {
                targetWriter.WriteLine("bleeding_restore_speed = " + str(value));
            }
            if (a.properties.TryGetValue(ArtifactProperty.HealthRestoreSpeed, out value))
            {
                targetWriter.WriteLine("health_restore_speed = " + str(value));
            }
            if (a.properties.TryGetValue(ArtifactProperty.PowerRestoreSpeed, out value))
            {
                targetWriter.WriteLine("power_restore_speed = " + str(value));
            }
            if (a.properties.TryGetValue(ArtifactProperty.SatietyRestoreSpeed, out value))
            {
                targetWriter.WriteLine("satiety_restore_speed = " + str(value));
            }

            //write immunities
            targetWriter.WriteLine("[af_" + id + "_absorbation]:af_base_absorbation");

            foreach (var prop in immunities)
            {
                if (a.properties.TryGetValue(prop, out value))
                {
                    targetWriter.WriteLine(immunityCodeNames[prop] + " = " + str(value));
                }
            }

            //write containers
            //aac

            targetWriter.WriteLine("[af_" + id + "_af_aac]:af_" + id + ", af_aac");
            targetWriter.WriteLine("class = SCRPTART");
            targetWriter.WriteLine("inv_weight=" + str(2.73f + a.Weight));
            targetWriter.WriteLine("cost = 0");
            targetWriter.WriteLine("can_trade = false");
            targetWriter.WriteLine("belt = true");
            targetWriter.WriteLine("description = " + StringDb.GetString("AAC with " + name));
            targetWriter.WriteLine("inv_name = " + StringDb.GetString("AAC with " + name));
            targetWriter.WriteLine("inv_name_short = " + StringDb.GetString("AAC with " + name));
            targetWriter.WriteLine("1icon_layer = af_" + id);
            targetWriter.WriteLine("radiation_restore_speed = " + str(Math.Max(0, a.RadiationRestoreSpeed - 0.00025f)));
            //iam
            targetWriter.WriteLine("[af_" + id + "_af_iam]:af_" + id + ", af_iam");
            targetWriter.WriteLine("class = SCRPTART");
            targetWriter.WriteLine("inv_weight=" + str(2.4f + a.Weight));
            targetWriter.WriteLine("cost = 0");
            targetWriter.WriteLine("can_trade = false");
            targetWriter.WriteLine("belt = true");
            targetWriter.WriteLine("description = " + StringDb.GetString("IAM with " + name));
            targetWriter.WriteLine("inv_name = " + StringDb.GetString("IAM with " + name));
            targetWriter.WriteLine("inv_name_short = " + StringDb.GetString("IAM with " + name));
            targetWriter.WriteLine("1icon_layer= af_" + id);
            targetWriter.WriteLine("radiation_restore_speed = " + str(Math.Max(0, a.RadiationRestoreSpeed - 0.00013f)));
            //llmc
            targetWriter.WriteLine("[af_" + id + "_lead_box]:lead_box_closed");
            targetWriter.WriteLine("inv_weight=" + str(2.12f + a.Weight));
            targetWriter.WriteLine("description = " + StringDb.GetString("LLMC with " + name));
            targetWriter.WriteLine("inv_name = " + StringDb.GetString("LLMC with " + name));
            targetWriter.WriteLine("inv_name_short = " + StringDb.GetString("LLMC with " + name));
            targetWriter.WriteLine("1icon_layer= af_" + id);
            targetWriter.WriteLine("radiation_restore_speed = " + str(Math.Max(0, a.RadiationRestoreSpeed - 0.00047f)));
            //aam
            targetWriter.WriteLine("[af_" + id + "_af_aam]:af_" + id + ", af_aam");
            targetWriter.WriteLine("class = SCRPTART");
            targetWriter.WriteLine("inv_weight=" + str(3.06f + a.Weight));
            targetWriter.WriteLine("cost = 0");
            targetWriter.WriteLine("can_trade = false");
            targetWriter.WriteLine("belt = true");
            targetWriter.WriteLine("description = " + StringDb.GetString("AAM with " + name));
            targetWriter.WriteLine("inv_name = " + StringDb.GetString("AAM with " + name));
            targetWriter.WriteLine("inv_name_short = " + StringDb.GetString("AAM with " + name));
            targetWriter.WriteLine("1icon_layer= af_" + id);
            targetWriter.WriteLine("radiation_restore_speed = " + str(Math.Max(0, a.RadiationRestoreSpeed - 0.00047f)));
            targetWriter.WriteLine();
        }
        public void Write(IEnumerable<Artifact> artifacts)
        {
            using (var fs = new FileStream(target, FileMode.Create, FileAccess.Write))
            using (targetWriter = new StreamWriter(fs))
            {
                foreach (var a in artifacts)
                {
                    WriteArtifact(a);
                }
            }
        }
    }
}
