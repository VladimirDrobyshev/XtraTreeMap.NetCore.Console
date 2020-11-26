using System.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing.Imaging;
using DevExpress.XtraTreeMap;
using DevExpress.XtraPrinting;

namespace XtraTreeMapNetCoreConsole {
    class Program {
        const string OutputPath = "Output";
        const int GroupsCount = 5;
        const int ItemsInGroupCount = 10;
        const int LabelLength = 20;

        static void Main(string[] args) {
            using (TreeMapControl treeMap = new TreeMapControl(800, 600)) {
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
            adapter.DataSource = CreateData();
            adapter.ValueDataMember = nameof(DataItem.Value);
            adapter.LabelDataMember = nameof(DataItem.Label);
            adapter.GroupDataMembers.Add(nameof(DataItem.Group));
            return adapter;
        }
        static List<DataItem> CreateData() {
            Random rnd = new Random(0);
            List<DataItem> data = new List<DataItem>();
            for (int i = 0; i < GroupsCount; i++)
                for (int j = 0; j < ItemsInGroupCount; j++)
                    data.Add(new DataItem(i, rnd.NextDouble() * 100, GetLabel(rnd)));
            return data;
        }
        static string GetLabel(Random rnd) {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < LabelLength; i++)
                builder.Append(rnd.Next('z' - 'A') + 'A');
            return builder.ToString();
        }
    }
}
