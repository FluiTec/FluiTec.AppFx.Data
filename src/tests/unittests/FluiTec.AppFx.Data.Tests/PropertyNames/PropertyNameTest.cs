using FluiTec.AppFx.Data.PropertyNames;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Data.Tests.PropertyNames;

[TestClass]
public class PropertyNameTest
{
    [TestMethod]
    [DataRow("CName1", "PName1")]
    [DataRow("CName2", "PName2")]
    public void CanSaveProperties(string columnName, string propertyName)
    {
        var name = new PropertyName(columnName, propertyName);
        Assert.AreEqual(propertyName, name.Name);
        Assert.AreEqual(columnName, name.ColumnName);
    }

    [TestMethod]
    [DataRow("CName1", null)]
    [DataRow("CName2", "")]
    [DataRow("CName3", " ")]
    [DataRow("CName4", "\t")]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ThrowsOnMissingPropertyName(string columnName, string propertyName)
    {
        var name = new PropertyName(columnName, propertyName);
    }

    [TestMethod]
    [DataRow(null, "PName1")]
    [DataRow("", "PName2")]
    [DataRow(" ", "PName3")]
    [DataRow("\t", "PName4")]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ThrowsOnMissingColumnNameName(string columnName, string propertyName)
    {
        var name = new PropertyName(columnName, propertyName);
    }

    [TestMethod]
    [DataRow("CName1", "PName1")]
    [DataRow("CName2", "PName2")]
    public void CreatesIdentitcalHashcode(string columnName, string propertyName)
    {
        var name1 = new PropertyName(columnName, propertyName);
        var name2 = new PropertyName(columnName, propertyName);

        Assert.AreEqual(name1.GetHashCode(), name2.GetHashCode());
    }

    [TestMethod]
    public void CanCompoareToPropertyName()
    {
        var name1 = new PropertyName("CName1", "PName1");
        var name2 = new PropertyName("CName1", "PName1");
        Assert.IsTrue(name1.Equals(name2));

        var name3 = new PropertyName("CName2", "Pname1");
        Assert.IsFalse(name1.Equals(name3));
        Assert.IsFalse(name2.Equals(name3));

        var name4 = new PropertyName("CName2", "Pname2");
        Assert.IsFalse(name1.Equals(name4));
        Assert.IsFalse(name2.Equals(name4));
        Assert.IsFalse(name3.Equals(name4));
    }

    [TestMethod]
    public void CanCompareToObject()
    {
        var name1 = new PropertyName("CName1", "PName1");
        var name2 = new PropertyName("CName1", "PName1");
        var name3 = new PropertyName("CName2", "PName2");
        Assert.IsFalse(name1.Equals(null!));

        // ReSharper disable once RedundantCast
        Assert.IsTrue(name1.Equals((object)name1));

        // ReSharper disable once RedundantCast
        Assert.IsTrue(name1.Equals((object)name2));

        // ReSharper disable once RedundantCast
        Assert.IsFalse(name1.Equals((object)name3));

        Assert.IsTrue(name1.Equals("CName1"));
        // ReSharper disable once SuspiciousTypeConversion.Global
        Assert.IsTrue(name1.Equals((object)"CName1"));
    }
}