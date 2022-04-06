using System.Linq;
using FluiTec.AppFx.Data.Sql.Attributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Data.Sql.Tests
{
    [TestClass]
    public class SqlCacheTest
    {
        [TestMethod]
        public void CanGetDefaultKey()
        {
            var keys = SqlCache.TypeKeyPropertiesCache(typeof(DefaultDummy));
            Assert.AreEqual(1,keys.Count);
            Assert.AreEqual(nameof(DefaultDummy.Id),keys.Single().Name);
        }

        [TestMethod]
        public void CanGetRenamedKey()
        {
            var keys = SqlCache.TypeKeyPropertiesCache(typeof(RenamedDummy));
            Assert.AreEqual(1, keys.Count);
            Assert.AreEqual(nameof(RenamedDummy.UId), keys.Single().Name);
        }

        [TestMethod]
        public void CanGetMultiKey()
        {
            var keys = SqlCache.TypeKeyPropertiesCache(typeof(MultiKeyDummy));
            Assert.AreEqual(2, keys.Count);
            Assert.IsTrue(keys.Any(k => k.Name == nameof(MultiKeyDummy.Id)));
            Assert.IsTrue(keys.Any(k => k.Name == nameof(MultiKeyDummy.MyKey)));
        }
    }

    public class DefaultDummy
    {
        public int Id { get; set; }
    }

    public class RenamedDummy
    {
        [SqlKey]
        public int UId { get; set; }
    }

    public class MultiKeyDummy
    {
        [SqlKey]
        public int Id { get; set; }

        [SqlKey]
        public int MyKey { get; set; }
    }
}