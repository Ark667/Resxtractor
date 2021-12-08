namespace Resxtractor.Test.Integration
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.IO;

    /// <summary>
    /// Defines the <see cref="ExtractOptions" />.
    /// </summary>
    [TestClass]
    [TestCategory("Integration")]
    public class ExtractOptions
    {
        /// <summary>
        /// The Extract.
        /// </summary>
        [TestMethod]
        public void Extract()
        {
            var extractor = new Options.ExtractOptions()
            {
                SourcePath = Path.GetFullPath(@"..\..\..\..\..\Resxtractor\Resxtractor.Tests.Template\Pages\Index.cshtml"),
                TargetResx = Path.GetFullPath(@"..\..\..\..\..\Resxtractor\Resxtractor.Tests.Template\Resources\Language.resx"),
                ReplaceMode = false,
            };

            extractor.Extract();
        }
    }
}
