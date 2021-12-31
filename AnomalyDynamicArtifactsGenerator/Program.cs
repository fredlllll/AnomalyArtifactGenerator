using System;
using System.Linq;

namespace AnomalyDynamicArtifactsGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            int finalAmount = 4500;
            if (args.Length > 0)
            {
                finalAmount = int.Parse(args[0]);
            }

            Console.WriteLine("Generating Artifacts...");

            ArtifactGenerator gen = new ArtifactGenerator("PropertyStats.txt");

            var arts5 = Thinners.ThinPercentage(gen.GetArtifacts(5), 0.01f).ToArray();
            var arts4 = Thinners.ThinPercentage(gen.GetArtifacts(4), 0.1f).ToArray();
            var arts3 = gen.GetArtifacts(3).ToArray();
            var arts = arts3.Concat(arts4).Concat(arts5);
            arts = Thinners.ThinPercentageHealthRestore(arts, 0.1f); //remove 90% of artifacts with health restore as its supposed to be rare
            arts = Thinners.ThinFixed(arts, finalAmount).ToArray();

            int gravi = 0, therm = 0, chem = 0, elec = 0, other = 0;
            foreach (var a in arts)
            {
                switch (a.ArtifactType)
                {
                    case ArtifactType.Gravi:
                        gravi++;
                        break;
                    case ArtifactType.Thermo:
                        therm++;
                        break;
                    case ArtifactType.Chem:
                        chem++;
                        break;
                    case ArtifactType.Electro:
                        elec++;
                        break;
                    default:
                        other++;
                        break;
                }
            }

            Console.WriteLine($"Gravi: {gravi} Thermo: {therm} Chem: {chem} Electro: {elec}");
            Console.WriteLine("Writing Files...");

            var itemsFileGen = new ItemsFileGenerator("gamedata\\configs\\items\\items\\items_dynart.ltx");
            itemsFileGen.Write(arts);

            var artifactsFileGen = new ArtifactsFileGenerator("gamedata\\configs\\items\\settings\\artefacts.ltx");
            artifactsFileGen.Write(arts);

            var devicesFileGen = new DevicesFileGenerator("gamedata\\configs\\items\\items\\items_devices.ltx");
            devicesFileGen.Write(arts);

            var uIDetectorArtefactFileGen = new UIDetectorArtefactFileGenerator("gamedata\\configs\\ui\\ui_detector_artefact.xml");
            uIDetectorArtefactFileGen.Write(arts);

            var stringsFileGen = new StringsFileGenerator("gamedata\\configs\\text\\eng\\st_dynart.xml");
            stringsFileGen.Write();

            Console.WriteLine("Done");
        }
    }
}
