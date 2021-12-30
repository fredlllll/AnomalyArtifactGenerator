using System.Collections.Generic;
using System.IO;

namespace AnomalyDynamicArtifactsGenerator
{
    public class UIDetectorArtefactFileGenerator
    {
        private readonly string target;
        private StreamWriter targetWriter;

        public UIDetectorArtefactFileGenerator(string target = "ui_detector_artefact.xml")
        {
            Directory.CreateDirectory(Path.GetDirectoryName(target));

            this.target = target;
        }

        public void Write(IEnumerable<Artifact> artifacts)
        {
            using (var fs = new FileStream(target, FileMode.Create, FileAccess.Write))
            using (targetWriter = new StreamWriter(fs))
            {
                targetWriter.WriteLine(File.ReadAllText("templates/ui_detector_artefact_1.xml"));

                foreach (var a in artifacts)
                {
                    targetWriter.WriteLine($"<palette id=\"af_{a.ID}\" width=\"0.005\" height=\"0.005\" stretch=\"1\" alignment=\"c\">");
                    targetWriter.WriteLine("<texture shader=\"hud\\p3d\">ui_inGame2_Detector_icon_artefact</texture>\r\n</palette>");
                }

                targetWriter.WriteLine(File.ReadAllText("templates/ui_detector_artefact_2.xml"));
            }
        }
    }
}
