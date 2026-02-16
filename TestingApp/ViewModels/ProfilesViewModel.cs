using System.Windows.Input;

namespace FileSorter.ViewModels
{
    public class ProfilesViewModel : ViewModelBase
    {
        public ICommand GoBackCommand { get; }

        public ProfilesViewModel(Action goBack)
        {
            GoBackCommand = new RelayCommand(goBack);
        }
    }
}
