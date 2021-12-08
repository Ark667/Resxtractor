namespace Resxtractor.Test.Unit
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
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
            Helpers.FilesHelper.IterateFiles(Path.GetFullPath(@"..\..\..\Resources"), (file) => { Debug.WriteLine(file); files.Add(file); });
            Assert.IsTrue(files.Count > 1);
            Assert.IsTrue(files.Contains(Path.GetFullPath(@"..\..\..\Resources\Inputs\Extract1.html")));

            // Iterate file
            files = new List<string>();
            Helpers.FilesHelper.IterateFiles(Path.GetFullPath(@"..\..\..\Resources\Inputs\Extract1.html"), (file) => { Debug.WriteLine(file); files.Add(file); });
            Assert.IsTrue(files.Count == 1);
            Assert.IsTrue(files.First() == Path.GetFullPath(@"..\..\..\Resources\Inputs\Extract1.html"));
        }

        /// <summary>
        /// The GetResxFile.
        /// </summary>
        [TestMethod]
        public void GetResxFile()
        {
            Assert.IsTrue(Helpers.FilesHelper.GetResxFile(
                Path.GetFullPath(@".\solution\project\resources"),
                Path.GetFullPath(@".\solution\project\views\view1.cshtml")) ==
                Path.GetFullPath(@".\solution\project\resources\views\views.resx"));

            Assert.IsTrue(Helpers.FilesHelper.GetResxFile(
                Path.GetFullPath(@".\solution\project\resources"),
                Path.GetFullPath(@".\solution\project\views\concept\view1.cshtml")) ==
                Path.GetFullPath(@".\solution\project\resources\concept\concept.resx"));
        }

        /// <summary>
        /// The GetNamespaceFromResxFile.
        /// </summary>
        [TestMethod]
        public void GetNamespaceFromResxFile()
        {
            Assert.IsTrue(Helpers.FilesHelper.GetNamespaceFromResxFile(
                Path.GetFullPath(@"..\..\..\Resources\GetNamespaceFromResxFile.resx")) ==
                "Resxtractor.Tests.Resources");

            Assert.ThrowsException<ArgumentException>(() => Helpers.FilesHelper.GetNamespaceFromResxFile(
                Path.GetFullPath(@"..\..\..\Resources\GetNamespaceFromResxFile1.resx")));
        }

        /// <summary>
        /// The CheckPath.
        /// </summary>
        [TestMethod]
        public void CheckPath()
        {
            // Existent folder
            Helpers.FilesHelper.CheckPath(
                Path.GetFullPath(@"..\..\..\Resources"));

            // Existent file
            Helpers.FilesHelper.CheckPath(
                Path.GetFullPath(@"..\..\..\Resources\GetNamespaceFromResxFile.resx"));

            // Non existent path
            Assert.ThrowsException<ArgumentException>(() => Helpers.FilesHelper.CheckPath(
                Path.GetFullPath(@"..\..\..\Nonexistent\GetNamespaceFromResxFile.resx")));
        }
    }
}
