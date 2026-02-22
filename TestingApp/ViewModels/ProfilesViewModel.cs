using System.Collections.ObjectModel;
using System.Windows;
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
        }


        //RULES
        public ObservableCollection<Rule> Rules { get; set; } = new ObservableCollection<Rule>();

        public ICommand AddRuleCommand { get; }
        public ICommand RemoveRuleCommand { get; }

        public ICommand AddExtensionCommand { get; }

        private void AddRule()
        {
            Rules.Add(new Rule());
        }

        private void RemoveRule(Rule rule)
        {
            if (rule != null && MessageBox.Show("Do you want to remove the rule: " + rule.TargetFolder + "?", "Remove rule", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Rules.Remove(rule);
            }
            
        }

        private void AddExtension(Rule rule) 
        {
            rule.Extensions.Add(new ExtensionItem());
            Console.WriteLine("Added");
        }



    }
}
