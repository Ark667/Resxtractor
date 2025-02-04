using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Resxtractor.Tests.Unit;

[TestClass]
public class FilesHelper
{
    [TestMethod]
    [TestCategory("Unit")]
    public void IterateFiles()
    {
        List<string> files;

        // Iterate directory
        files = new List<string>();
        Helpers.FilesHelper.IterateFiles(
            Path.GetFullPath(@"../../../Resources"),
            (file) =>
            {
                Debug.WriteLine(file);
                files.Add(file);
            }
        );
        Assert.IsTrue(files.Count > 1);
        Assert.IsTrue(files.Contains(Path.GetFullPath(@"../../../Resources/Inputs/Extract1.html")));

        // Iterate file
        files = new List<string>();
        Helpers.FilesHelper.IterateFiles(
            Path.GetFullPath(@"../../../Resources/Inputs/Extract1.html"),
            (file) =>
            {
                Debug.WriteLine(file);
                files.Add(file);
            }
        );
        Assert.AreEqual(1, files.Count);
        Assert.AreEqual(
            Path.GetFullPath(@"../../../Resources/Inputs/Extract1.html"),
            files.First()
        );
    }

    [TestMethod]
    public void GetResxFile()
    {
        Assert.AreEqual(
            Path.GetFullPath(@"./solution/project/resources/views/views.resx"),
            Helpers.FilesHelper.GetResxFile(
                Path.GetFullPath(@"./solution/project/resources"),
                Path.GetFullPath(@"./solution/project/views/view1.cshtml")
            )
        );

        Assert.AreEqual(
            Path.GetFullPath(@"./solution/project/resources/concept/concept.resx"),
            Helpers.FilesHelper.GetResxFile(
                Path.GetFullPath(@"./solution/project/resources"),
                Path.GetFullPath(@"./solution/project/views/concept/view1.cshtml")
            )
        );
    }

    [TestMethod]
    public void GetNamespaceFromResxFile()
    {
        Assert.AreEqual(
            "",
            Helpers.FilesHelper.GetNamespaceFromResxFile(
                Path.GetFullPath(@"../../../Resources/GetNamespaceFromResxFile.resx")
            )
        );

        Assert.ThrowsException<ArgumentException>(
            () =>
                Helpers.FilesHelper.GetNamespaceFromResxFile(
                    Path.GetFullPath(@"../../../Resources/GetNamespaceFromResxFile1.resx")
                )
        );
    }

    [TestMethod]
    public void CheckPath()
    {
        // Existent folder
        Helpers.FilesHelper.CheckPath(Path.GetFullPath(@"../../../Resources"));

        // Existent file
        Helpers.FilesHelper.CheckPath(
            Path.GetFullPath(@"../../../Resources/GetNamespaceFromResxFile.resx")
        );

        // Non existent path
        Assert.ThrowsException<ArgumentException>(
            () =>
                Helpers.FilesHelper.CheckPath(
                    Path.GetFullPath(@"../../../Nonexistent/GetNamespaceFromResxFile.resx")
                )
        );
    }
}
