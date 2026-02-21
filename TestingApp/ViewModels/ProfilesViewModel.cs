using System.Collections.ObjectModel;
using System.Windows.Input;
using FileSorter.Rule_system;

namespace FileSorter.ViewModels
{
    public class ProfilesViewModel : ViewModelBase
    {
        public ICommand GoBackCommand { get; }

        public ProfilesViewModel(Action goBack)
        {
            GoBackCommand = new RelayCommand(goBack);
            AddRuleCommand = new RelayCommand(AddRule);
            RemoveRuleCommand = new RelayCommand<Rule>(RemoveRule);
        }


        //RULES
        public ObservableCollection<Rule> Rules { get; set; } = new ObservableCollection<Rule>();

        public ICommand AddRuleCommand { get; }
        public ICommand RemoveRuleCommand { get; }


        private void AddRule()
        {
            Rules.Add(new Rule());
        }

        private void RemoveRule(Rule rule)
        {
            if (rule != null)
            {
                Rules.Remove(rule);
            }
            
        }

    }
}
