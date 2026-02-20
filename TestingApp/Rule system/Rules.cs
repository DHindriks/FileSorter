using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FileSorter.Rule_system
{
    public class Rule
    {
        public string TargetFolder { get; set; }
        public ObservableCollection<string> Extensions { get; set; } = new ObservableCollection<string>();
    }

    public class RuleSet
    {
        public ObservableCollection<Rule> rules { get; set; } = new ObservableCollection<Rule> ();
    }
}
