using Microsoft.VisualStudio.TestTools.UnitTesting;
using Resxtractor.Services;
using System.IO;

namespace Resxtractor.Tests.Unit;

[TestClass]
[TestCategory("Unit")]
public class ExtractHelper
{
    [TestMethod]
    public void Extract()
    {
        var helper = new ExtractService(
            Path.GetFullPath(@"../../../Resources/Inputs/Extract1.html"),
            replace: false,
            @namespace: "SomeNamespace"
        );
        var result = helper.Extract(Path.GetFullPath(@"../../../Resources/SomeResx.resx"));

        Assert.AreEqual(1, result.Count);
        Assert.AreEqual("Hola", result["Extract1_0"]);
        Assert.IsTrue(
            File.Exists(Path.GetFullPath(@"../../../Resources/Inputs/Extract1.auto.html"))
        );
        Assert.IsTrue(
            File.ReadAllText(Path.GetFullPath(@"../../../Resources/Inputs/Extract1.auto.html"))
                .Contains("<p>@SomeNamespace.SomeResx_auto.Extract1_0</p>")
        );
    }

    [TestMethod]
    public void GetReferenceName()
    {
        var helper = new ExtractService(
            Path.GetFullPath(@"../../../Resources/Inputs/Extract1.html"),
            @namespace: "SomeNamespace"
        );

        // No replace (auto)
        helper.Replace = false;
        Assert.AreEqual(
            "@SomeNamespace.SomeResx_auto.Extract1_0",
            helper.GetReferenceName(
                Path.GetFullPath(@"../../../Resources/SomeResx.resx"),
                "Extract1_0"
            )
        );

        // Replace
        helper.Replace = true;
        Assert.AreEqual(
            "@SomeNamespace.SomeResx.Extract1_0",
            helper.GetReferenceName(
                Path.GetFullPath(@"../../../Resources/SomeResx.resx"),
                "Extract1_0"
            )
        );

        // Namespace from designer
        helper.Namespace = null;
        Assert.AreEqual(
            "@.GetReferenceName.Extract1_0",
            helper.GetReferenceName(
                Path.GetFullPath(@"../../../Resources/GetReferenceName.resx"),
                "Extract1_0"
            )
        );
    }
}
