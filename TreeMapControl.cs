using System;
using System.Drawing;
using DevExpress.TreeMap.Native;
using DevExpress.XtraTreeMap.Native;

namespace XtraTreeMapNetCoreConsole {
    public class TreeMapControl : IDisposable {
        InnerTreeMap innerTreeMap;

        public InnerTreeMap InnerTreeMap { get { return innerTreeMap; } }

        public TreeMapControl(int width, int height) {
            innerTreeMap = new InnerTreeMap();
            innerTreeMap.BackColor = Color.White;

            innerTreeMap.SetClientRectangle(new Rectangle(0, 0, width, height));
            innerTreeMap.Printer.DeviceIndependentPixel = true;
            //innerTreeMap.Printer.ImageFormat = PrintImageFormat.Metafile;
        }
        public void Dispose() {
            if (innerTreeMap != null) {
                innerTreeMap.Dispose();
                innerTreeMap = null;
            }
        }
    }
}