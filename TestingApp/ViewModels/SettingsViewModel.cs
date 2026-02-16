using Microsoft.Win32;
using System.Net.NetworkInformation;
using System.Windows;
using System.Windows.Input;

namespace FileSorter.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        public ICommand GoBackCommand { get; }
        public ICommand PickTargetFolderCommand { get; }

        private string _targetfolder = "Target folder: " + Properties.Settings.Default.TargetFolder;

        public SettingsViewModel(Action goBack)
        {
            GoBackCommand = new RelayCommand(goBack);
            PickTargetFolderCommand = new RelayCommand(PickTargetFolder);
            OnPropertyChanged(nameof(TargetFolder));
        }

        public string TargetFolder
        {
            get => _targetfolder;
            set
            {
                if (_targetfolder == value)
                    return;

                _targetfolder = value;
                OnPropertyChanged(nameof(TargetFolder));
            }
        }

        private void PickTargetFolder()
        {
            var dialog = new OpenFolderDialog
            {
                Title = "Select target folder",
                Multiselect = false,                     
            };

            bool? result = dialog.ShowDialog(Application.Current.MainWindow);

            if (result == true)
            {
                string selectedPath = dialog.FolderName;

                //save picked path
                Properties.Settings.Default.TargetFolder = selectedPath;
                Properties.Settings.Default.Save();

                //notify user and update UI
                TargetFolder = "Target folder: " + Properties.Settings.Default.TargetFolder;
                MessageBox.Show($"Folder set to: \n{selectedPath}");
            }
        }
    }
}
