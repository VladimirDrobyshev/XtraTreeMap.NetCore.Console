namespace XtraTreeMapNetCoreConsole {
    public class DataItem {
        public double Value { get; set; }
        public string Label { get; set; }
        public int Group { get; set; }

        public DataItem(int group, double value, string label) {
            Value = value;
            Group = group;
            Label = label;
        }
    }
}