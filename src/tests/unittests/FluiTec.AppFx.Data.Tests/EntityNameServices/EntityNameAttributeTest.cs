using FluiTec.AppFx.Data.EntityNameServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Data.Tests.EntityNameServices
{
    [TestClass]
    public class EntityNameAttributeTest
    {
        [TestMethod]
        [DataRow("Test")]
        public void CanSetName(string name)
        {
            var attribute = new EntityNameAttribute(name);
            Assert.AreEqual(name, attribute.Name);
        }
    }
}