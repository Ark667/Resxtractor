namespace Resxtractor.Helpers
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Defines the <see cref="FilesHelper" />.
    /// </summary>
    public static class FilesHelper
    {
        /// <summary>
        /// The IterateFiles.
        /// </summary>
        /// <param name="sourcePath">The sourcePath<see cref="string"/>.</param>
        /// <param name="actionOnFile">The action<see cref="Action"/>.</param>
        public static void IterateFiles(string sourcePath, Action<string> actionOnFile)
        {
            string[] sourceFiles;
            if (File.GetAttributes(sourcePath).HasFlag(FileAttributes.Directory))
            {
                sourceFiles = Directory.EnumerateFileSystemEntries(sourcePath).ToArray();
            }
            else
            {
                sourceFiles = new string[] { sourcePath };
            }

            foreach (var sourceFile in sourceFiles)
            {
                if (File.GetAttributes(sourceFile).HasFlag(FileAttributes.Directory))
                {
                    IterateFiles(sourceFile, actionOnFile);
                }
                else
                {
                    actionOnFile.Invoke(sourceFile);
                }
            }
        }

        /// <summary>
        /// The GetResourceFile.
        /// </summary>
        /// <param name="resxFolder">The resourceDir<see cref="string"/>.</param>
        /// <param name="sourceFile">The file<see cref="string"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public static string GetResxFile(string resxFolder, string sourceFile)
        {
            string[] sourceFilePath = Path.GetDirectoryName(sourceFile).Split('\\');
            if (!Directory.Exists($"{resxFolder}\\{sourceFilePath.Last()}"))
                Directory.CreateDirectory($"{resxFolder}\\{sourceFilePath.Last()}");

            string resxFile = $"{resxFolder}\\{sourceFilePath.Last()}\\{sourceFilePath.Last()}.resx";
            Debug.WriteLine($"Guessing resx file '{resxFile}'");

            return resxFile;
        }

        /// <summary>
        /// The CheckPath.
        /// </summary>
        /// <param name="sourcePath">The sourcePath<see cref="string"/>.</param>
        public static void CheckPath(string sourcePath)
        {
            if (!Directory.Exists(sourcePath) && !File.Exists(sourcePath))
                throw new ArgumentException($"Path '{Path.GetFullPath(sourcePath)}' not exists.");
        }

        /// <summary>
        /// The GetNamespaceFromResxFile.
        /// </summary>
        /// <param name="resxFile">The resxFile<see cref="string"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public static string GetNamespaceFromResxFile(string resxFile)
        {
            var designerFile = resxFile.Replace(Path.GetExtension(resxFile), ".Designer.cs");
            if (!File.Exists(designerFile)) throw new ArgumentException($"File {Path.GetFullPath(designerFile)} not exists, make sure VS is generating this file");
            var content = File.ReadAllText(designerFile);

            return Regex.Match(content, "namespace (.*) {").Groups[1].Value;
        }
    }
}
