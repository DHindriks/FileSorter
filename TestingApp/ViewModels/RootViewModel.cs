namespace FileSorter.ViewModels
{
    public class RootViewModel : ViewModelBase
    {
        private readonly ISorter _sorter;

        public ViewModelBase CurrentViewModel { get; private set; }

        private MainViewModel _mainVm;
        private SettingsViewModel _settingsVm;
        private ProfilesViewModel _profilesVm;

        public RootViewModel()
        {
            if (_sorter == null)
            {
                _sorter = new Sorter();
            }
            ShowMain();
        }

        private void ShowMain()
        {
            _mainVm ??= new MainViewModel(
                _sorter,
                ShowSettings,
                ShowProfiles);

            CurrentViewModel = _mainVm;
            OnPropertyChanged(nameof(CurrentViewModel));
        }

        private void ShowSettings()
        {
            _settingsVm ??= new SettingsViewModel(ShowMain);
            CurrentViewModel = _settingsVm;
            OnPropertyChanged(nameof(CurrentViewModel));
        }

        private void ShowProfiles()
        {
            _profilesVm ??= new ProfilesViewModel(ShowMain);
            CurrentViewModel = _profilesVm;
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
