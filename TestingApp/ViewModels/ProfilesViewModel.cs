using FileSorter.Rule_system;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace FileSorter.ViewModels
{
    public class ProfilesViewModel : ViewModelBase
    {
        public ICommand GoBackCommand { get; }

        public ProfilesViewModel(RuleService ruleService, Action goBack)
        {
            _ruleService = ruleService;
            GoBackCommand = new RelayCommand(() => { RequestSave(); goBack(); });
            AddRuleCommand = new RelayCommand(AddRule);
            SaveRulesCommand = new RelayCommand(SaveRules);
            RemoveRuleCommand = new RelayCommand<Rule>(RemoveRule);
            AddExtensionCommand = new RelayCommand<Rule>(AddExtension);
        }


        //RULES
        private readonly RuleService _ruleService;

        public ObservableCollection<Rule> Rules => _ruleService.Rules;

        public ICommand AddRuleCommand { get; }
        public ICommand SaveRulesCommand { get; }
        public ICommand RemoveRuleCommand { get; }

        public ICommand AddExtensionCommand { get; }

        private bool changesMade = false;

        private void AddRule()
        {
            Rules.Add(new Rule());
            changesMade = true;
        }

        private void RemoveRule(Rule rule)
        {
            if (rule != null && MessageBox.Show("Do you want to remove the rule: " + rule.TargetFolder + "?", "Remove rule", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Rules.Remove(rule);
            }
            changesMade = true;
        }

        private void RequestSave()
        {
            if (changesMade && MessageBox.Show("You have unsaved changes, save rules?", "Save rules", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                SaveRules(); 
            }else
            {
                _ruleService.Load();
            }
            changesMade = false;
        }

       private void SaveRules()
        {
            _ruleService.Save();
            changesMade = false;
        }

        private void AddExtension(Rule rule) 
        {
            rule.Extensions.Add(new ExtensionItem());
            changesMade = true;
        }



    }
}
