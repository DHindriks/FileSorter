using FileSorter.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace FileSorter.Rule_system
{
    public class Rule
    {
        public string TargetFolder { get; set; }
        public ObservableCollection<ExtensionItem> Extensions { get; set; } = new ObservableCollection<ExtensionItem>();
    }

    public class ExtensionItem : INotifyPropertyChanged
    {
        private string _value;

        public string Value
        {
            get => _value;
            set
            {
                if (_value == value) return;
                _value = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Value)));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
