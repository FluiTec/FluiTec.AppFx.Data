using System;
using FluiTec.AppFx.Data.EntityNameServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Data.Tests.EntityNameServices
{
    [TestClass]
    public class AttributeEntityNameServiceTest
    {
        [TestMethod]
        [DataRow(typeof(Dummy))]
        public void CanNameEntityByClass(Type type)
        {
            Assert.AreEqual(nameof(Dummy), EntityNameService.GetDefault().Name(type));
        }

        [TestMethod]
        [DataRow("Test", typeof(AttributedDummy))]
        public void CanNameEntityByAttribute(string name, Type type)
        {
            Assert.AreEqual(name, EntityNameService.GetDefault().Name(type));
        }

        public class Dummy
        {
        }

        [EntityName("Test")]
        public class AttributedDummy
        {
        }
    }
}