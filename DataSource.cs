using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

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

    public static class DataLoader {
        const int GroupsCount = 5;
        const int ItemsInGroupCount = 10;
        const int LabelLength = 20;

        static string GetLabel(Random rnd) {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < LabelLength; i++)
                builder.Append((char)(rnd.Next('z' - 'A') + 'A'));
            return builder.ToString();
        }
        public static List<DataItem> CreateRandomData() {
            Random rnd = new Random(0);
            List<DataItem> data = new List<DataItem>();
            for (int i = 0; i < GroupsCount; i++)
                for (int j = 0; j < ItemsInGroupCount; j++)
                    data.Add(new DataItem(i, rnd.NextDouble() * 100, GetLabel(rnd)));
            return data;
        }
        public static List<ShipItem> GetShipData() {
            Dictionary<Tuple<string, string>, ShipItem> sortedData = new Dictionary<Tuple<string, string>, ShipItem>();
            using (StreamReader reader = new StreamReader("Data/Orders.csv")) {
                string country;
                string name;
                int via;
                string line = reader.ReadLine();
                while (!string.IsNullOrEmpty(line)) {
                    line = reader.ReadLine();
                    if (!string.IsNullOrEmpty(line)) {
                        string[] items = line.Split(',');
                        country = items[0];
                        name = items[1];
                        via = Convert.ToInt32(items[2]);
                        Tuple<string, string> key = new Tuple<string, string>(country, name);
                        ShipItem item;
                        if (!sortedData.ContainsKey(key)) {
                            item = new ShipItem(country, name);
                            sortedData.Add(key, item);
                        }
                        else
                            item = sortedData[key];
                        item.IncreaseVia(via);
                    }
                }
            }
            return new List<ShipItem>(sortedData.Values);
        }
    }

    public class ShipItem {
        public string ShipCountry { get; private set; }
        public string ShipName { get; private set; }
        public int ShipVia { get; private set; }

        public  ShipItem(string country, string name) {
            ShipCountry = country;
            ShipName = name;
            ShipVia = 0;
        }
        public void IncreaseVia(int via) {
            ShipVia += via;
        }
    }
}