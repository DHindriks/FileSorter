using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Text.Json;

namespace FileSorter.Rule_system
{
    public class RuleService
    {
        private const string FilePath = "rules.json";

        public ObservableCollection<Rule> Rules { get; private set; }
            = new ObservableCollection<Rule>();

        public void Save()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            var json = JsonSerializer.Serialize(Rules, options);
            File.WriteAllText(FilePath, json);
        }

        public void Load()
        {
            if (!File.Exists(FilePath))
                return;

            var json = File.ReadAllText(FilePath);
            var loaded = JsonSerializer.Deserialize<ObservableCollection<Rule>>(json);

            if (loaded != null)
                Rules = loaded;
        }
    }
}
