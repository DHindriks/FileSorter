using FileSorter.Rule_system;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileSorter
{

    public class Sorter : ISorter
    {
        private readonly RuleService _ruleService;

        public Sorter(RuleService ruleService) 
        {
            _ruleService = ruleService;
        }

        private static string NormalizeExtension(string extension)
        {
            if (string.IsNullOrWhiteSpace(extension))
                return string.Empty;

            extension = extension.Trim().ToLowerInvariant();

            if (!extension.StartsWith("."))
                extension = "." + extension;

            return extension;
        }

        public void Sort(Action<string> reportStatus)
        {
            string downloadsPath = Properties.Settings.Default.TargetFolder;

            // creates all target folders
            foreach (var rule in _ruleService.Rules)
            {
                if (!string.IsNullOrWhiteSpace(rule.TargetFolder))
                {
                    string targetDir = Path.Combine(downloadsPath, rule.TargetFolder);
                    Directory.CreateDirectory(targetDir);
                }
            }

            string miscDir = Path.Combine(downloadsPath, "miscellaneous");
            Directory.CreateDirectory(miscDir);

            // Build extension lookup
            var extensionLookup = _ruleService.Rules
                .Where(r => !string.IsNullOrWhiteSpace(r.TargetFolder))
                .SelectMany(rule => rule.Extensions
                    .Where(e => !string.IsNullOrWhiteSpace(e.Value))
                    .Select(e => new
                    {
                        Extension = NormalizeExtension(e.Value),
                        Rule = rule
                    }))
                .GroupBy(x => x.Extension, StringComparer.OrdinalIgnoreCase)
                .ToDictionary(
                    g => g.Key,
                    g => g.First().Rule,
                    StringComparer.OrdinalIgnoreCase);

            foreach (var file in Directory.GetFiles(downloadsPath))
            {
                string extension = Path.GetExtension(file);

                if (string.Equals(extension, ".crdownload", StringComparison.OrdinalIgnoreCase)) //Ignores active downloads
                    continue;

                if (extensionLookup.TryGetValue(extension, out var matchingRule))
                {
                    string targetDir = Path.Combine(downloadsPath, matchingRule.TargetFolder);
                    Directory.CreateDirectory(targetDir); //creates folder if it doesn't exist yet TODO: create setting in settings menu: Create empty folders.

                    reportStatus($"Moving file: {file} to: {targetDir}");
                    MoveFileSafely(file, targetDir);
                }
                else //Unsorted file types go into misc folder
                {
                    Directory.CreateDirectory(miscDir); 

                    reportStatus($"Moving file: {file} to: {miscDir}");
                    MoveFileSafely(file, miscDir);
                }
            }
        }

        private static void MoveFileSafely(string sourceFile, string targetDirectory)
        {
            string fileName = Path.GetFileName(sourceFile);
            string destinationPath = Path.Combine(targetDirectory, fileName);

            // Avoid overwriting existing files
            if (File.Exists(destinationPath))
            {
                string name = Path.GetFileNameWithoutExtension(fileName);
                string ext = Path.GetExtension(fileName);
                destinationPath = Path.Combine(
                    targetDirectory,
                    $"{name}_{DateTime.Now.Ticks}{ext}"
                );
            }
            File.Move(sourceFile, destinationPath);
        }
    }
}
