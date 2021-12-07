namespace Resxtractor.Test.Unit
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    /// <summary>
    /// Defines the <see cref="FilesHelper" />.
    /// </summary>
    [TestClass]
    public class FilesHelper
    {
        /// <summary>
        /// The IterateFiles.
        /// </summary>
        [TestMethod]
        [TestCategory("Unit")]
        public void IterateFiles()
        {
            List<string> files;

            // Iterate directory
            files = new List<string>();
            Helpers.FilesHelper.IterateFiles(@"..\..\..\Resources", (file) => { Debug.WriteLine(file); files.Add(file); });
            Assert.IsTrue(files.Count > 1);
            Assert.IsTrue(files.Contains(@"..\..\..\Resources\Inputs\Extract1.html"));

            // Iterate file
            files = new List<string>();
            Helpers.FilesHelper.IterateFiles(@"..\..\..\Resources\Inputs\Extract1.html", (file) => { Debug.WriteLine(file); files.Add(file); });
            Assert.IsTrue(files.Count == 1);
            Assert.IsTrue(files.First() == @"..\..\..\Resources\Inputs\Extract1.html");
        }

        /// <summary>
        /// The GetResxFile.
        /// </summary>
        [TestMethod]
        public void GetResxFile()
        {
            Assert.IsTrue(Helpers.FilesHelper.GetResxFile(
                @".\solution\project\resources",
                @".\solution\project\views\view1.cshtml") ==
                @".\solution\project\resources\views\views.resx");

            Assert.IsTrue(Helpers.FilesHelper.GetResxFile(
                @".\solution\project\resources",
                @".\solution\project\views\concept\view1.cshtml") ==
                @".\solution\project\resources\concept\concept.resx");
        }

        /// <summary>
        /// The GetNamespaceFromResxFile.
        /// </summary>
        [TestMethod]
        public void GetNamespaceFromResxFile()
        {
            Assert.IsTrue(Helpers.FilesHelper.GetNamespaceFromResxFile(
                @"..\..\..\Resources\GetNamespaceFromResxFile.resx") ==
                "Resxtractor.Tests.Resources");

            Assert.ThrowsException<ArgumentException>(() => Helpers.FilesHelper.GetNamespaceFromResxFile(
                @"..\..\..\Resources\GetNamespaceFromResxFile1.resx"));
        }

        /// <summary>
        /// The CheckPath.
        /// </summary>
        [TestMethod]
        public void CheckPath()
        {
            // Existent folder
            Helpers.FilesHelper.CheckPath(
                @"..\..\..\Resources");

            // Existent file
            Helpers.FilesHelper.CheckPath(
                @"..\..\..\Resources\GetNamespaceFromResxFile.resx");

            // Non existent path
            Assert.ThrowsException<ArgumentException>(() => Helpers.FilesHelper.CheckPath(
                @"..\..\..\Nonexistent\GetNamespaceFromResxFile.resx"));
        }
    }
}
