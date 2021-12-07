namespace Resxtractor.Test.Integration
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

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
                SourcePath = @"..\..\..\..\..\Resxtractor\Resxtractor.Tests.Template\Pages\Index.cshtml",
                TargetResx = @"..\..\..\..\..\Resxtractor\Resxtractor.Tests.Template\Resources\Language.resx",
                ReplaceMode = false,
            };

            extractor.Extract();
        }
    }
}
