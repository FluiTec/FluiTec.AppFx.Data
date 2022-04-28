using System.Linq;
using FluiTec.AppFx.Data.Sql.Tests.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Data.Sql.Tests
{
    [TestClass]
    public class SqlCacheTest
    {
        [TestMethod]
        public void CanGetDefaultKey()
        {
            var keys = SqlCache.TypeKeyPropertiesCache(typeof(Dummy));
            Assert.AreEqual(1,keys.Count);
            Assert.AreEqual(nameof(Dummy.Id),keys.Single().PropertyInfo.Name);
        }

        [TestMethod]
        public void CanGetRenamedKey()
        {
            var keys = SqlCache.TypeKeyPropertiesCache(typeof(RenamedDummy));
            Assert.AreEqual(1, keys.Count);
            Assert.AreEqual(nameof(RenamedDummy.UId), keys.Single().PropertyInfo.Name);
        }

        [TestMethod]
        public void CanGetMultiKey()
        {
            var keys = SqlCache.TypeKeyPropertiesCache(typeof(MultiKeyDummy));
            Assert.AreEqual(2, keys.Count);
            Assert.IsTrue(keys.Any(k => k.PropertyInfo.Name == nameof(MultiKeyDummy.Id)));
            Assert.IsTrue(keys.Any(k => k.PropertyInfo.Name == nameof(MultiKeyDummy.MyKey)));
        }
    }
}