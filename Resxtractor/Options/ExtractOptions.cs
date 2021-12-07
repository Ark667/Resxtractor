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
        [Option('s', "sourcePath", HelpText = "Source path to files that will be extracted.", Default = ".", Required = true)]
        public string SourcePath { get; set; }

        /// <summary>
        /// Gets or sets the TargetPath.
        /// </summary>
        [Option('t', "targetPath", HelpText = "Target path where .resx files will be created.", Default = ".", Required = true)]
        public string TargetResx { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether ReplaceMode.
        /// </summary>
        [Option('r', "repalceMode", HelpText = "Make file replacements, otherwise new files will be created for debug pourposes.", Default = true)]
        public bool ReplaceMode { get; set; }

        /// <summary>
        /// Gets or sets the SourceFilesRegex.
        /// </summary>
        [Option('x', "sourceFilesRegex", HelpText = "Source files must match this regex condition.", Default = "html")]
        public string SourceFilesRegex { get; set; }

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
            });

            return 0;
        }
    }
}
