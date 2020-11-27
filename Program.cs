using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing.Imaging;
using DevExpress.XtraTreeMap;
using DevExpress.XtraPrinting;

namespace XtraTreeMapNetCoreConsole {
    class Program {
        const string OutputPath = "Output";

        static void Main(string[] args) {
            using (TreeMapControl treeMap = new TreeMapControl(947, 660)) {
                treeMap.InnerTreeMap.DataAdapter = CreateDataAdapter();
                TreeMapGroupGradientColorizer colorizer = new TreeMapGroupGradientColorizer();
                colorizer.Palette = Palettes.GetPalette("Office");
                treeMap.InnerTreeMap.Colorizer = colorizer;

                if (!Directory.Exists(OutputPath))
                    Directory.CreateDirectory(OutputPath);

                treeMap.InnerTreeMap.Printer.ExportToImage(OutputPath + "/Test.png", ImageFormat.Png);
                PdfExportOptions options = new PdfExportOptions();
                options.ConvertImagesToJpeg = false;
                treeMap.InnerTreeMap.Printer.ExportToPdf(OutputPath + "/Test.pdf", options);
            }
            
            Console.WriteLine("Done.");
        }
        static TreeMapFlatDataAdapter CreateDataAdapter() {
            TreeMapFlatDataAdapter adapter = new TreeMapFlatDataAdapter();
            adapter.DataSource = DataLoader.GetShipData();
            adapter.ValueDataMember = nameof(ShipItem.ShipVia);
            adapter.LabelDataMember = nameof(ShipItem.ShipName);
            adapter.GroupDataMembers.Add(nameof(ShipItem.ShipCountry));
            return adapter;
        }
    }
}
