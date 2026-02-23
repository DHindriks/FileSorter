using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Text.Json;
using System.Windows;

namespace FileSorter.Rule_system
{
    public class RuleService
    {
        private const string FilePath = "rules.json";

        public ObservableCollection<Rule> Rules { get; private set; }
            = new ObservableCollection<Rule>();

        public void Save(string path = FilePath)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            var json = JsonSerializer.Serialize(Rules, options);
            File.WriteAllText(path, json);
        }

        public void Load(string path = FilePath)
        {
            if (!File.Exists(path))
            {
                return;
            }

            var json = File.ReadAllText(path);
            var loaded = JsonSerializer.Deserialize<ObservableCollection<Rule>>(json);

            if (loaded != null)
            {
                Rules.Clear();

                foreach (var rule in loaded)
                {
                    Rules.Add(rule);
                }
            }
        }
    }
}
