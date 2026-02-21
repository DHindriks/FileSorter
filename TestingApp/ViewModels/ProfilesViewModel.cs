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
            AddExtensionCommand = new RelayCommand<Rule>(AddExtension);
            RemoveExtensionCommand = new RelayCommand(RemoveExtension);
        }


        //RULES
        public ObservableCollection<Rule> Rules { get; set; } = new ObservableCollection<Rule>();

        public ICommand AddRuleCommand { get; }
        public ICommand RemoveRuleCommand { get; }

        public ICommand AddExtensionCommand { get; }
        public ICommand RemoveExtensionCommand { get; }

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

        private void AddExtension(Rule rule) 
        {
            rule.Extensions.Add(new ExtensionItem());
            Console.WriteLine("Added");
        }

        private void RemoveExtension() 
        {

        }

    }
}
