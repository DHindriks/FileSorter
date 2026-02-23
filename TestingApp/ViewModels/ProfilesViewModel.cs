using FileSorter.Rule_system;
using Microsoft.Win32;
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

            ImportCommand = new RelayCommand(ImportRules);
            ExportCommand = new RelayCommand(ExportRules);
        }


        //RULES
        private readonly RuleService _ruleService;

        public ObservableCollection<Rule> Rules => _ruleService.Rules;

        public ICommand AddRuleCommand { get; }
        public ICommand SaveRulesCommand { get; }
        public ICommand RemoveRuleCommand { get; }

        public ICommand AddExtensionCommand { get; }

        public ICommand ImportCommand { get; }
        public ICommand ExportCommand { get; }

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
            MessageBox.Show("Rules Saved.");
        }

        private void AddExtension(Rule rule) 
        {
            rule.Extensions.Add(new ExtensionItem());
            changesMade = true;
        }

        private void ImportRules ()
        {
            var dialog = new OpenFileDialog
            {
                Title = "Import Rules",
                Filter = "JSON files (*.json)|*.json"
            };

            if (dialog.ShowDialog() == true)
            {
                _ruleService.Load(dialog.FileName);
                changesMade = true;
                MessageBox.Show("Ruleset imported.");
            }
        }

        private void ExportRules()
        {
            var dialog = new SaveFileDialog
            {
                Title = "Export ruleset file",
                Filter = "JSON files (*.json)|*.json",
                DefaultExt = ".json",
                AddExtension = true,
                FileName = "rules.json"

            };

            if (dialog.ShowDialog() == true)
            {
                _ruleService.Save(dialog.FileName);
                MessageBox.Show("Ruleset exported.");
            }
        }
    }
}
