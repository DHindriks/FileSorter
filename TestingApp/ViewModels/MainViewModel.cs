using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace FileSorter.ViewModels
{
    public class MainViewModel : ViewModelBase
    {

        private readonly ISorter _sorter;
        private string _status = string.Empty;

        public ICommand SortCommand { get; }
        public ICommand OpenSettingsCommand { get; }
        public ICommand OpenProfilesCommand { get; }

        public string StatusText
        {
            get => _status;
            set
            {
                if (_status == value)
                    return;

                _status = value;
                OnPropertyChanged(nameof(StatusText));
            }
        }

        public MainViewModel(ISorter sorter, Action OpenSettings, Action OpenProfiles)
        {
            _sorter = sorter;
            SortCommand = new RelayCommand(Sort);
            OpenSettingsCommand = new RelayCommand(OpenSettings);
            OpenProfilesCommand = new RelayCommand(OpenProfiles);
        }



        private void Sort()
        {
            StatusText = string.Empty; //clears any previous outputs
            StatusText += "\n" + "Sorting downloads...";
            _sorter.Sort(msg => StatusText += "\n" + msg);
            StatusText += "\n" + "Done!";
            MessageBox.Show("Sorting complete.");
        }
    }
}
