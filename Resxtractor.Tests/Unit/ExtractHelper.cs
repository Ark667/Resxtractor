namespace Resxtractor.Test.Unit
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.IO;

    /// <summary>
    /// Defines the <see cref="ExtractHelper" />.
    /// </summary>
    [TestClass]
    [TestCategory("Unit")]
    public class ExtractHelper
    {
        /// <summary>
        /// The Extract.
        /// </summary>
        [TestMethod]
        public void Extract()
        {
            var helper = new Helpers.ExtractHelper(Path.GetFullPath(@"../../../Resources/Inputs/Extract1.html"), replace: false, @namespace: "SomeNamespace");
            var result = helper.Extract(Path.GetFullPath(@"../../../Resources/SomeResx.resx"));

            Assert.IsTrue(result.Count == 1);
            Assert.IsTrue(result["Extract1_0"] == "Hola");
            Assert.IsTrue(File.Exists(Path.GetFullPath(@"../../../Resources/Inputs/Extract1.auto.html")));
            Assert.IsTrue(File.ReadAllText(Path.GetFullPath(@"../../../Resources/Inputs/Extract1.auto.html")).Contains("<p>@SomeNamespace.SomeResx_auto.Extract1_0</p>"));
        }
        /// <summary>
        /// The GetReferenceName.
        /// </summary>
        [TestMethod]
        public void GetReferenceName()
        {
            var helper = new Helpers.ExtractHelper(Path.GetFullPath(@"../../../Resources/Inputs/Extract1.html"), @namespace: "SomeNamespace");

            // No replace (auto)
            helper.Replace = false;
            Assert.IsTrue(helper.GetReferenceName(Path.GetFullPath(@"../../../Resources/SomeResx.resx"), "Extract1_0") == "@SomeNamespace.SomeResx_auto.Extract1_0");

            // Replace
            helper.Replace = true;
            Assert.IsTrue(helper.GetReferenceName(Path.GetFullPath(@"../../../Resources/SomeResx.resx"), "Extract1_0") == "@SomeNamespace.SomeResx.Extract1_0");

            // Namespace from designer
            helper.Namespace = null;
            Assert.IsTrue(helper.GetReferenceName(Path.GetFullPath(@"../../../Resources/GetReferenceName.resx"), "Extract1_0") == "@Resxtractor.Tests.Resources.GetReferenceName.Extract1_0");
        }
    }
}
