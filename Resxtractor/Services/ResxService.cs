using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Resources.NetStandard;

namespace Resxtractor.Services;

/// <summary>
/// Defines the <see cref="ResxService" />.
/// </summary>
public class ResxService
{
    /// <summary>
    /// Gets the ResxFile.
    /// </summary>
    public string ResxFile { get; set; }

    /// <summary>
    /// Gets a value indicating whether ReplaceMode.
    /// </summary>
    public bool Replace { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ResxService"/> class.
    /// </summary>
    /// <param name="resxFile">The resxFile<see cref="string"/>.</param>
    /// <param name="replace">The replaceMode<see cref="bool"/>.</param>
    public ResxService(string resxFile, bool replace = true)
    {
        ResxFile = resxFile;
        Replace = replace;
    }

    /// <summary>
    /// The UpdateResxFile.
    /// </summary>
    /// <param name="resource">The resource<see cref="Dictionary{string, string}"/>Key value pairs to add to the file.</param>
    public void UpdateResxFile(Dictionary<string, string> resource = null)
    {
        string targetResxFile = ResxFile;
        if (!Replace)
            targetResxFile = targetResxFile.Replace(
                Path.GetExtension(targetResxFile),
                $".auto{Path.GetExtension(targetResxFile)}"
            );

        using (var writer = new ResXResourceWriter(targetResxFile))
        {
            // Preserve current keys
            if (File.Exists(ResxFile))
            {
                using (var reader = new ResXResourceReader(ResxFile))
                {
                    var enumerator = reader.GetEnumerator();
                    while (enumerator.MoveNext())
                        if (resource == null || !resource.ContainsKey(enumerator.Key.ToString()))
                            writer.AddResource(
                                enumerator.Key.ToString(),
                                enumerator.Value.ToString()
                            );
                }
            }

            // Add new keys to resx file
            if (resource != null)
                foreach (var key in resource.Keys)
                    writer.AddResource(key, resource[key]);

            writer.Generate();
            writer.Close();
        }
    }

    /// <summary>
    /// The GetKeyFromValue.
    /// </summary>
    /// <param name="value">The htmlValue<see cref="string"/>.</param>
    /// <returns>The <see cref="string"/>.</returns>
    public string GetKeyFromValue(string value)
    {
        using (var reader = new ResXResourceReader(ResxFile))
        {
            Debug.WriteLine($"Loaded {Path.GetFullPath(ResxFile)}");

            var enumerator = reader.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (value == enumerator.Value.ToString())
                    return enumerator.Key.ToString();
            }

            return null;
        }
    }
}
