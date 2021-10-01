using System;
using FluiTec.AppFx.Data.EntityNameServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Data.Tests.EntityNameServices
{
    [TestClass]
    public class ClassEntityNameServiceTest
    {
        [TestMethod]
        [DataRow(typeof(Dummy))]
        public void CanNameEntity(Type type)
        {
            var nameService = new ClassEntityNameService();
            Assert.AreEqual(nameof(Dummy), nameService.Name(type));
        }

        public class Dummy
        {
        }
    }
}