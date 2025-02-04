using CommandLine;
using Resxtractor.Helpers;
using Resxtractor.Services;
using System;
using System.IO;

namespace Resxtractor.Options;

/// <summary>
/// Defines the <see cref="ExtractOptions" />.
/// </summary>
[Verb("extract", HelpText = "Extract text literals from html code to .resx file.")]
public class ExtractOptions
{
    /// <summary>
    /// Gets or sets the SourcePath.
    /// </summary>
    [Option(
        's',
        "sourcePath",
        HelpText = "Source path to files that will be extracted.",
        Required = true
    )]
    public string SourcePath { get; set; }

    /// <summary>
    /// Gets or sets the TargetResx.
    /// </summary>
    [Option(
        't',
        "targetResx",
        HelpText = "Target path where .resx file will be updated.",
        Required = true
    )]
    public string TargetResx { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether ReplaceMode.
    /// </summary>
    [Option(
        'r',
        "replaceMode",
        HelpText = "Make .cshtml and .resx file replacements, otherwise new files will be created for debug pourposes."
    )]
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
        FilesHelper.IterateFiles(
            SourcePath,
            (sourceFile) =>
            {
                var extractor = new ExtractService(sourceFile, ReplaceMode);
                new ResxService(TargetResx, ReplaceMode).UpdateResxFile(
                    extractor.Extract(TargetResx)
                );
                Console.WriteLine($"Extracted '{Path.GetFullPath(sourceFile)}'");
            }
        );

        Console.WriteLine($"Updated '{Path.GetFullPath(TargetResx)}'");
        Console.WriteLine(
            $"Ensure public access modifier on VS to create {Path.GetFileNameWithoutExtension(TargetResx)}.Designer.cs"
        );

        return 0;
    }
}
