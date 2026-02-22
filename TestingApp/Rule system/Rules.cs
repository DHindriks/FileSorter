using FileSorter.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;

namespace FileSorter.Rule_system
{
    public class Rule
    {
        public string TargetFolder { get; set; }
        public ObservableCollection<ExtensionItem> Extensions { get; set; } = new ObservableCollection<ExtensionItem>();

        public ICommand RemoveExtensionCommand { get; }

        public Rule() 
        {
            RemoveExtensionCommand = new RelayCommand<ExtensionItem>(RemoveExtension);
        }

        private void RemoveExtension(ExtensionItem extension)
        {
            Extensions.Remove(extension);
        }
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
