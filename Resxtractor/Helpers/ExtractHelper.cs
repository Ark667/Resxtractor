namespace Resxtractor.Helpers
{
    using AngleSharp;
    using AngleSharp.Dom;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Defines the <see cref="ExtractHelper" />.
    /// </summary>
    public class ExtractHelper
    {
        /// <summary>
        /// Gets the SourceFile.
        /// </summary>
        public string SourceFile { get; set; }

        /// <summary>
        /// Gets a value indicating whether Replace.
        /// </summary>
        public bool Replace { get; set; }

        /// <summary>
        /// Gets the Namespace.
        /// </summary>
        public string Namespace { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExtractHelper"/> class.
        /// </summary>
        /// <param name="sourceFile">The sourceFile<see cref="string"/>.</param>
        /// <param name="replace">The replace<see cref="bool"/>.</param>
        /// <param name="@namespace">The namespace<see cref="string"/>.</param>
        public ExtractHelper(string sourceFile, bool replace = true, string @namespace = null)
        {
            SourceFile = sourceFile;
            Replace = replace;
            Namespace = @namespace;
        }

        /// <summary>
        /// The GetReferences.
        /// </summary>
        /// <param name="resxFile">The resxFile<see cref="string"/>.</param>
        /// <returns>The <see cref="Dictionary{string, string}"/>.</returns>
        public Dictionary<string, string> Extract(string resxFile)
        {
            Stream stream = File.OpenRead(SourceFile);
            var context = BrowsingContext.New(Configuration.Default);
            var document = context.OpenAsync(o => o.Content(stream)).Result;

            // Load source file content
            string contents = document.Source.Text;
            contents = Regex.Replace(contents, "(?<!\r)\r", "");

            var regx = new Regex(@"[(*)][)]");
            var nodes = document.Body.Descendents()
                .Where(o => o.NodeType == NodeType.Text
                && !(o.ParentElement is AngleSharp.Html.Dom.IHtmlScriptElement)
                && o.Text().Trim() != ""
                && !o.Text().Contains("}")
                && !o.Text().Contains("{")
                && !o.Text().Contains("//")
                && !o.Text().Contains("@")
                && !regx.IsMatch(o.Text().Trim())
                && !(o.Text().Trim().Length == 1 && Char.IsPunctuation(o.Text().Trim()[0])))
                .ToList();

            var keys = new Dictionary<string, string>();

            if (nodes != null)
            {
                int id = 0;
                foreach (var node in nodes)
                {
                    string key = $"{Path.GetFileNameWithoutExtension(SourceFile)}_{id}";
                    string value = node.Text().Trim();
                    keys.Add(key, value);
                    id++;

                    // Replace literal with resx reference key
                    var currentValue = node.ParentElement.OuterHtml;
                    node.TextContent = node.TextContent.Replace(node.Text().Trim(), GetReferenceName(resxFile, key));
                    contents = contents.ReplaceFirst(currentValue, node.ParentElement.OuterHtml);
                }
            }

            stream.Close();

            // Update source file
            if (nodes != null && nodes.Count > 0)
            {
                var extractedFile = SourceFile;
                if (!Replace) extractedFile = extractedFile.Replace(Path.GetExtension(extractedFile), $".auto{Path.GetExtension(extractedFile)}");
                File.WriteAllText(extractedFile, contents);
            }

            return keys;
        }

        /// <summary>
        /// The GetReferenceName.
        /// </summary>
        /// <param name="resxFile">The resxFile<see cref="string"/>.</param>
        /// <param name="key">The key<see cref="string"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public string GetReferenceName(string resxFile, string key)
        {
            var namespaceName = Namespace ?? FilesHelper.GetNamespaceFromResxFile(resxFile);
            var className = Path.GetFileNameWithoutExtension(resxFile) + (Replace ? string.Empty : "_auto");
            var referenceValue = $"@{namespaceName}.{className}.{key}";
            return referenceValue;
        }
    }
}
