using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluiTec.AppFx.Data.Sql.Adapters;
using FluiTec.AppFx.Data.Sql.Tests.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Data.Sql.Tests.Adapters
{
    /// <summary>
    /// A SQL adapter test.
    /// </summary>
    public abstract class SqlAdapterTest
    {
        /// <summary>
        /// Gets the adapter.
        /// </summary>
        ///
        /// <value>
        /// The adapter.
        /// </value>
        public ISqlAdapter Adapter { get; }

        /// <summary>
        /// Specialized constructor for use only by derived class.
        /// </summary>
        ///
        /// <param name="adapter">  The adapter. </param>
        protected SqlAdapterTest(ISqlAdapter adapter)
        {
            Adapter = adapter;
        }

        protected abstract string GetExpectedKeyParameters(Type entityType);

        /// <summary>
        /// (Unit Test Method) tests get key parameters.
        /// </summary>
        ///
        /// <param name="entityType">   Type of the entity. </param>
        [TestMethod]
        [DataRow(typeof(Dummy))]
        [DataRow(typeof(RenamedDummy))]
        [DataRow(typeof(MultiKeyDummy))]
        public void GetKeyParametersTest(Type entityType)
        {
            var parameters = Adapter.GetKeyParameters(entityType);
            Assert.AreEqual(GetExpectedKeyParameters(entityType), parameters);
        }

        protected abstract string GetExpectedSelectAllStatement(Type entityType);

        /// <summary>
        /// (Unit Test Method) tests select all statement.
        /// </summary>
        ///
        /// <param name="entityType">   Type of the entity. </param>
        [TestMethod]
        [DataRow(typeof(Dummy))]
        [DataRow(typeof(RenamedDummy))]
        [DataRow(typeof(MultiKeyDummy))]
        public void SelectAllStatementTest(Type entityType)
        {
            var statement = Adapter.SelectAllStatement(entityType);
            Assert.AreEqual(GetExpectedSelectAllStatement(entityType), statement);
        }

        protected abstract string GetExpectedByKeyStatement(Type entityType);

        /// <summary>
        /// (Unit Test Method) tests get by key statement.
        /// </summary>
        ///
        /// <param name="entityType">   Type of the entity. </param>
        [TestMethod]
        [DataRow(typeof(Dummy))]
        [DataRow(typeof(RenamedDummy))]
        [DataRow(typeof(MultiKeyDummy))]
        public void GetByKeyStatementTest(Type entityType)
        {
            var statement = Adapter.GetByKeyStatement(entityType);
            Assert.AreEqual(GetExpectedByKeyStatement(entityType), statement);
        }

        protected abstract string GetExpectedByFilterStatement(Type entityType, string filterProperty, string[] selectProperties);

        /// <summary>
        /// (Unit Test Method) tests get by filter statement.
        /// </summary>
        ///
        /// <param name="entityType">       Type of the entity. </param>
        /// <param name="filterProperty">   The filter property. </param>
        /// <param name="selectProperties"> The select properties. </param>
        [TestMethod]
        [DataRow(typeof(Dummy), "Name", new [] {"Id", "Name"})]
        [DataRow(typeof(RenamedDummy), "Name", new[] {"UId", "Name"})]
        [DataRow(typeof(MultiKeyDummy), "MyKey", new[] {"Id", "MyKey"})]
        public void GetByFilterStatementTest(Type entityType, string filterProperty, string[] selectProperties)
        {
            var statement = Adapter.GetByFilterStatement(entityType, filterProperty, selectProperties);
            Assert.AreEqual(GetExpectedByFilterStatement(entityType, filterProperty, selectProperties), statement);
        }

        protected abstract string GetExpectedByFilterInStatement(Type entityType, string filterProperty, string colName, string[] selectFields);

        /// <summary>
        /// (Unit Test Method) tests get by filter in statement.
        /// </summary>
        ///
        /// <param name="entityType">       Type of the entity. </param>
        /// <param name="filterProperty">   The filter property. </param>
        /// <param name="colName">          Name of the col. </param>
        /// <param name="selectFields">     The select fields. </param>
        [TestMethod]
        [DataRow(typeof(Dummy), "Name", "Names", new[] { "Id", "Name" })]
        [DataRow(typeof(RenamedDummy), "Name", "Names", new[] { "UId", "Name" })]
        [DataRow(typeof(MultiKeyDummy), "MyKey", "Keys", new[] { "Id", "MyKey" })]
        public void GetByFilterInStatementTest(Type entityType, string filterProperty, string colName, string[] selectFields)
        {
            var statement = Adapter.GetByFilterInStatement(entityType, filterProperty, colName, selectFields);
            Assert.AreEqual(GetExpectedByFilterInStatement(entityType, filterProperty, colName, selectFields), statement);
        }

        protected abstract string GetExpectedByFilterStatement(Type entityType, string[] properties);

        /// <summary>
        /// (Unit Test Method) tests get by multi filter statement.
        /// </summary>
        ///
        /// <param name="entityType">   Type of the entity. </param>
        /// <param name="_">            The. </param>
        /// <param name="properties">   The properties. </param>
        [TestMethod]
        [DataRow(typeof(Dummy), "_", new[] { "Id", "Name" })]
        [DataRow(typeof(RenamedDummy), "_", new[] { "UId", "Name" })]
        [DataRow(typeof(MultiKeyDummy), "_", new[] { "Id", "MyKey" })]
        public void GetByMultiFilterStatementTest(Type entityType, string _, string[] properties)
        {
            var statement = Adapter.GetByFilterStatement(entityType, properties, properties);
            Assert.AreEqual(GetExpectedByFilterStatement(entityType, properties), statement);
        }

        protected abstract string GetExpectedInsertStatement(Type entityType);

        /// <summary>
        /// (Unit Test Method) tests get insert statement.
        /// </summary>
        ///
        /// <param name="entityType">   Type of the entity. </param>
        [TestMethod]
        [DataRow(typeof(Dummy))]
        [DataRow(typeof(RenamedDummy))]
        [DataRow(typeof(MultiKeyDummy))]
        public void GetInsertStatementTest(Type entityType)
        {
            var statement = Adapter.GetInsertStatement(entityType);
            Assert.AreEqual(GetExpectedInsertStatement(entityType), statement);
        }
        
        protected abstract string GetExpectedInsertAutoKeyStatement(Type entityType);

        /// <summary>
        /// (Unit Test Method) tests get insert automatic key statement.
        /// </summary>
        ///
        /// <param name="entityType">   Type of the entity. </param>
        [TestMethod]
        [DataRow(typeof(Dummy))]
        [DataRow(typeof(RenamedDummy))]
        public void GetInsertAutoKeyStatementTest(Type entityType)
        {
            var statement = Adapter.GetInsertAutoKeyStatement(entityType);
            Assert.AreEqual(GetExpectedInsertAutoKeyStatement(entityType), statement);
        }

        protected abstract string GetExpectedInsertMultipleStatement(Type entityType);

        /// <summary>
        /// (Unit Test Method) tests get insert multiple statement.
        /// </summary>
        ///
        /// <param name="entityType">   Type of the entity. </param>
        [TestMethod]
        [DataRow(typeof(Dummy))]
        [DataRow(typeof(RenamedDummy))]
        [DataRow(typeof(MultiKeyDummy))]
        public void GetInsertMultipleStatementTest(Type entityType)
        {
            var statement = Adapter.GetInsertMultipleStatement(entityType);
            Assert.AreEqual(GetExpectedInsertMultipleStatement(entityType), statement);
        }

        protected abstract string GetExpectedInsertAutoKeyMultipleStatement(Type entityType);

        /// <summary>
        /// (Unit Test Method) tests get insert automatic key multiple statement.
        /// </summary>
        ///
        /// <param name="entityType">   Type of the entity. </param>
        [TestMethod]
        [DataRow(typeof(Dummy))]
        [DataRow(typeof(RenamedDummy))]
        public void GetInsertAutoKeyMultipleStatementTest(Type entityType)
        {
            var statement = Adapter.GetInsertAutoKeyMultipleStatement(entityType);
            Assert.AreEqual(GetExpectedInsertAutoKeyMultipleStatement(entityType), statement);
        }

        protected abstract string GetExpectedUpdateStatement(Type entityType);

        /// <summary>
        /// (Unit Test Method) tests get update statement.
        /// </summary>
        ///
        /// <param name="entityType">   Type of the entity. </param>
        [TestMethod]
        [DataRow(typeof(Dummy))]
        [DataRow(typeof(RenamedDummy))]
        [DataRow(typeof(MultiKeyDummy))]
        public void GetUpdateStatementTest(Type entityType)
        {
            var statement = Adapter.GetUpdateStatement(entityType);
            Assert.AreEqual(GetExpectedUpdateStatement(entityType), statement);
        }

        protected abstract string GetExpectedDeleteStatement(Type entityType);

        /// <summary>
        /// (Unit Test Method) tests get delete statememt.
        /// </summary>
        ///
        /// <param name="entityType">   Type of the entity. </param>
        [TestMethod]
        [DataRow(typeof(Dummy))]
        [DataRow(typeof(RenamedDummy))]
        [DataRow(typeof(MultiKeyDummy))]
        public void GetDeleteStatememtTest(Type entityType)
        {
            var statement = Adapter.GetDeleteStatememt(entityType);
            Assert.AreEqual(GetExpectedDeleteStatement(entityType), statement);
        }

        protected abstract string GetExpectedDeleteByStatement(Type entityType, string filterProperty);

        /// <summary>
        /// (Unit Test Method) tests get delete by statememt.
        /// </summary>
        ///
        /// <param name="entityType">       Type of the entity. </param>
        /// <param name="filterProperty">   The filter property. </param>
        [TestMethod]
        [DataRow(typeof(Dummy), "Name")]
        [DataRow(typeof(RenamedDummy), "Name")]
        [DataRow(typeof(MultiKeyDummy), "MyKey")]
        public void GetDeleteByStatememtTest(Type entityType, string filterProperty)
        {
            var statement = Adapter.GetDeleteByStatememt(entityType, filterProperty);
            Assert.AreEqual(GetExpectedDeleteByStatement(entityType, filterProperty), statement);
        }

        protected abstract string RenderExpectedPropertyList(PropertyInfo[] props, Type entityType = null);

        /// <summary>
        /// (Unit Test Method) renders the property list test described by entityType.
        /// </summary>
        ///
        /// <param name="entityType">   Type of the entity. </param>
        [TestMethod]
        [DataRow(typeof(Dummy))]
        [DataRow(typeof(RenamedDummy))]
        [DataRow(typeof(MultiKeyDummy))]
        public void RenderPropertyListTest(Type entityType)
        {
            var props = SqlCache.TypePropertiesChache(entityType).ToArray();
            var output = Adapter.RenderPropertyList(props).ToString();
            Assert.AreEqual(RenderExpectedPropertyList(props), output);
        }

        /// <summary>
        /// (Unit Test Method) renders the property list test 1 described by entityType.
        /// </summary>
        ///
        /// <param name="entityType">   Type of the entity. </param>
        [TestMethod]
        [DataRow(typeof(Dummy))]
        [DataRow(typeof(RenamedDummy))]
        [DataRow(typeof(MultiKeyDummy))]
        public void RenderPropertyListTest1(Type entityType)
        {
            var props = SqlCache.TypePropertiesChache(entityType).ToArray();
            var output = Adapter.RenderPropertyList(entityType, props).ToString();
            Assert.AreEqual(RenderExpectedPropertyList(props, entityType), output);
        }

        protected abstract string GetExpectedRenderTableName(Type entityType);

        /// <summary>
        /// (Unit Test Method) renders the table name test described by entityType.
        /// </summary>
        ///
        /// <param name="entityType">   Type of the entity. </param>
        [TestMethod]
        [DataRow(typeof(Dummy))]
        [DataRow(typeof(RenamedDummy))]
        [DataRow(typeof(MultiKeyDummy))]
        public void RenderTableNameTest(Type entityType)
        {
            var tableName = Adapter.RenderTableName(entityType);
            Assert.AreEqual(GetExpectedRenderTableName(entityType), tableName);
        }

        protected abstract string GetExpectedRenderPropertyName(string propName);

        /// <summary>
        /// (Unit Test Method) renders the property name test described by entityType.
        /// </summary>
        ///
        /// <param name="entityType">   Type of the entity. </param>
        [TestMethod]
        [DataRow(typeof(Dummy))]
        [DataRow(typeof(RenamedDummy))]
        [DataRow(typeof(MultiKeyDummy))]
        public void RenderPropertyNameTest(Type entityType)
        {
            foreach (var p in SqlCache.TypePropertiesChache(entityType))
            {
                Assert.AreEqual(GetExpectedRenderPropertyName(p.Name), Adapter.RenderPropertyName(p));
            }
        }

        /// <summary>
        /// (Unit Test Method) renders the property name test 1 described by entityType.
        /// </summary>
        ///
        /// <param name="entityType">   Type of the entity. </param>
        [TestMethod]
        [DataRow(typeof(Dummy))]
        [DataRow(typeof(RenamedDummy))]
        [DataRow(typeof(MultiKeyDummy))]
        public void RenderPropertyNameTest1(Type entityType)
        {
            foreach (var p in SqlCache.TypePropertiesChache(entityType))
            {
                Assert.AreEqual(GetExpectedRenderPropertyName(p.Name), Adapter.RenderPropertyName(p.Name));
            }
        }

        protected abstract string GetExpectedRenderParameterPropertyName(string pName);

        /// <summary>
        /// (Unit Test Method) renders the parameter property test described by entityType.
        /// </summary>
        ///
        /// <param name="entityType">   Type of the entity. </param>
        [TestMethod]
        [DataRow(typeof(Dummy))]
        [DataRow(typeof(RenamedDummy))]
        [DataRow(typeof(MultiKeyDummy))]
        public void RenderParameterPropertyTest(Type entityType)
        {
            foreach (var p in SqlCache.TypePropertiesChache(entityType))
            {
                Assert.AreEqual(GetExpectedRenderParameterPropertyName(p.Name), Adapter.RenderParameterProperty(p));
            }
        }

        /// <summary>
        /// (Unit Test Method) renders the parameter property test 1 described by entityType.
        /// </summary>
        ///
        /// <param name="entityType">   Type of the entity. </param>
        [TestMethod]
        [DataRow(typeof(Dummy))]
        [DataRow(typeof(RenamedDummy))]
        [DataRow(typeof(MultiKeyDummy))]
        public void RenderParameterPropertyTest1(Type entityType)
        {
            foreach (var p in SqlCache.TypePropertiesChache(entityType))
            {
                Assert.AreEqual(GetExpectedRenderParameterPropertyName(p.Name), Adapter.RenderParameterProperty(p.Name));
            }
        }
    }
}