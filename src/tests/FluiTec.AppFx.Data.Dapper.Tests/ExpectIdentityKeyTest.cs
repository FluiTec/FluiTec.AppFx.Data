using System;
using FluiTec.AppFx.Data.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Data.Dapper.Tests
{
    /// <summary>   (Unit Test Class) a dapper writable key table data repository test.</summary>
    [TestClass]
    public class ExpectIdentityKeyTest
    {
        /// <summary>   (Unit Test Method) tests unique identifier repository.</summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestGuid()
        {
            var entity = new TestEntity();
            if (entity.Id.Equals(GetDefault(typeof(Guid))))
                throw new InvalidOperationException("EntityKey must be set to a non default value.");
        }

        /// <summary>   Gets a default.</summary>
        /// <param name="type"> The type. </param>
        /// <returns>   The default.</returns>
        private static object GetDefault(Type type)
        {
            System.Console.WriteLine(type.IsValueType);
            return type.IsValueType ? Activator.CreateInstance(type) : null;
        }

        /// <summary>   (Unit Test Class) a test entity.</summary>
        protected class TestEntity : IKeyEntity<Guid>
        {
            public Guid Id { get; set; }
        }
    }
}