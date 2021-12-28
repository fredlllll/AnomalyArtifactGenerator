using System;
using System.Collections.Generic;
using System.Linq;

namespace AnomalyDynamicArtifactsGenerator
{
    class Program
    {


        static void Main(string[] args)
        {
            ArtifactGenerator gen = new ArtifactGenerator("PropertyStats.txt");

            var arts = gen.GetArtifacts(4);
            arts = arts.Where((a) => { return Util.r.NextDouble() > 0.9; }).ToArray(); //to array to only evaluate once

            var itemsFileGen = new ItemsFileGenerator("gamedata\\configs\\items\\items\\items_dynart.ltx");
            itemsFileGen.Write(arts);

            var artifactsFileGen = new ArtifactsFileGenerator("gamedata\\configs\\items\\settings\\artefacts.ltx");
            artifactsFileGen.Write(arts);

            var devicesFileGen = new DevicesFileGenerator("gamedata\\configs\\items\\items\\items_devices.ltx");
            devicesFileGen.Write(arts);

            var uIDetectorArtefactFileGen = new UIDetectorArtefactFileGenerator("gamedata\\configs\\ui\\ui_detector_artefact.xml");
            uIDetectorArtefactFileGen.Write(arts);

            Console.WriteLine("Generated Artifacts: " + arts.Count());
        }
    }
}
