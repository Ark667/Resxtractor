using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace Resxtractor.Tests.Integration;

[TestClass]
[TestCategory("Integration")]
public class ExtractOptions
{
    [TestMethod]
    public void Extract()
    {
        var extractor = new Options.ExtractOptions()
        {
            SourcePath = Path.GetFullPath(
                @"../../../../../Resxtractor/Resxtractor.Tests.Template/Pages/Index.cshtml"
            ),
            TargetResx = Path.GetFullPath(
                @"../../../../../Resxtractor/Resxtractor.Tests.Template/Resources/Language.resx"
            ),
            ReplaceMode = false,
        };

        extractor.Extract();
    }
}
