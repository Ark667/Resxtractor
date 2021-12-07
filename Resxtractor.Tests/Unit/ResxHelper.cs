namespace Resxtractor.Test.Unit
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.IO;

    /// <summary>
    /// Defines the <see cref="ResxHelper" />.
    /// </summary>
    [TestClass]
    [TestCategory("Unit")]
    public class ResxHelper
    {
        /// <summary>
        /// The UpdateResxFile.
        /// </summary>
        [TestMethod]
        public void UpdateResxFile()
        {
            var helper = new Helpers.ResxHelper(@"..\..\..\Resources\UpdateResxFile.resx");

            // Empty file
            File.Delete(helper.ResxFile);
            Assert.IsTrue(!File.Exists(helper.ResxFile));
            helper.UpdateResxFile();
            Assert.IsTrue(File.Exists(helper.ResxFile));

            // New file with keys
            File.Delete(helper.ResxFile);
            Assert.IsTrue(!File.Exists(helper.ResxFile));
            helper.UpdateResxFile(new System.Collections.Generic.Dictionary<string, string>() { { "String1", "test1" }, { "String2", "test2" } });
            Assert.IsTrue(File.Exists(helper.ResxFile));
            Assert.IsTrue(helper.GetKeyFromValue("test2") == "String2");
            Assert.IsTrue(helper.GetKeyFromValue("test") == null);

            // Update files
            Assert.IsTrue(File.Exists(helper.ResxFile));
            helper.UpdateResxFile(new System.Collections.Generic.Dictionary<string, string>() { { "String2", "test22" }, { "String3", "test3" } });
            Assert.IsTrue(File.Exists(helper.ResxFile));
            Assert.IsTrue(helper.GetKeyFromValue("test3") == "String3");
            Assert.IsTrue(helper.GetKeyFromValue("test22") == "String2");
            Assert.IsTrue(helper.GetKeyFromValue("test2") == null);
            
            helper.Replace = false;
            File.Delete(@"..\..\..\Resources\UpdateResxFile.auto.resx");
            Assert.IsTrue(!File.Exists(@"..\..\..\Resources\UpdateResxFile.auto.resx"));
            helper.UpdateResxFile();
            Assert.IsTrue(File.Exists(@"..\..\..\Resources\UpdateResxFile.auto.resx"));
        }

        /// <summary>
        /// The GetKeyFromValue.
        /// </summary>
        [TestMethod]
        public void GetKeyFromValue()
        {
            var helper = new Helpers.ResxHelper(@"..\..\..\Resources\GetKeyFromValue.resx");

            Assert.IsTrue(helper.GetKeyFromValue("test") == "String1");
            Assert.IsTrue(helper.GetKeyFromValue("fail") == null);
        }
    }
}
