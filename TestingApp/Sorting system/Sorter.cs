using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileSorter
{

    public class Sorter : ISorter
    {
        public void Sort(Action<string> reportStatus)
        {
            string downloadsPath = Properties.Settings.Default.TargetFolder;

            // Category → extensions
            var rules = new Dictionary<string, string[]>
            {
                ["java"] = new[] { ".jar" },

                ["zip"] = new[] { ".zip", ".rar", ".7z" },

                ["images"] = new[] { ".jpg", ".jpeg", ".png", ".gif", ".ico", ".webp", ".xcf" },

                ["executables"] = new[] { ".exe", ".iso", ".msi" },

                ["text"] = new[] { ".txt", ".doc", ".rtf", ".pptx", ".xlsx", ".pdf" },

                ["sound"] = new[] { ".mp3", ".wav", ".ogg", ".m4a" },

                ["videos"] = new[] { ".mp4", ".webm" }
            };

            // Process each category
            foreach (var rule in rules)
            {
                string targetDir = Path.Combine(downloadsPath, rule.Key);
                Directory.CreateDirectory(targetDir);

                foreach (var file in Directory.GetFiles(downloadsPath))
                {
                    string extension = Path.GetExtension(file).ToLowerInvariant();

                    if (rule.Value.Contains(extension))
                    {
                        reportStatus("Moving file: " + file.ToString() + " to: " + targetDir);
                        MoveFileSafely(file, targetDir);
                    }
                }
            }

            // Miscellaneous (everything else)
            string miscDir = Path.Combine(downloadsPath, "miscellaneous");
            Directory.CreateDirectory(miscDir);

            foreach (var file in Directory.GetFiles(downloadsPath))
            {
                if (Path.GetExtension(file).ToLowerInvariant() != ".crdownload") //ignore active downloads
                {
                    reportStatus("Moving file: " + file.ToString() + " to: " + miscDir);
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
