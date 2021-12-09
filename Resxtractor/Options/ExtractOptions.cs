namespace Resxtractor.Options
{
    using CommandLine;
    using Resxtractor.Helpers;
    using System;
    using System.IO;

    /// <summary>
    /// Defines the <see cref="ExtractOptions" />.
    /// </summary>
    [Verb("extract", HelpText = "Extract text literals from html code to .resx file.")]
    public class ExtractOptions
    {
        /// <summary>
        /// Gets or sets the SourcePath.
        /// </summary>
        [Option('s', "sourcePath", HelpText = "Source path to files that will be extracted.", Required = true)]
        public string SourcePath { get; set; }

        /// <summary>
        /// Gets or sets the targetResx.
        /// </summary>
        [Option('t', "targetResx", HelpText = "Target path where .resx files will be created.", Required = true)]
        public string TargetResx { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether ReplaceMode.
        /// </summary>
        [Option('r', "replaceMode", HelpText = "Make file replacements, otherwise new files will be created for debug pourposes.", Default = true)]
        public bool ReplaceMode { get; set; }

        /// <summary>
        /// The Extract.
        /// </summary>
        /// <returns>The <see cref="int"/>Zero if successful.</returns>
        public int Extract()
        {
            // Check paths
            FilesHelper.CheckPath(SourcePath);
            FilesHelper.CheckPath(TargetResx);

            // Execute process
            FilesHelper.IterateFiles(SourcePath, (sourceFile) =>
            {
                var extractor = new ExtractHelper(sourceFile, ReplaceMode);
                new ResxHelper(TargetResx, ReplaceMode).UpdateResxFile(extractor.Extract(TargetResx));
                Console.WriteLine($"Extracted '{sourceFile}'");
            });

            Console.WriteLine($"Updated '{Path.GetFullPath(TargetResx)}'");
            Console.WriteLine($"Set access modifier to let VS create {Path.GetFileNameWithoutExtension(TargetResx)}.Designer.cs");

            return 0;
        }
    }
}
