using System.Data;
using FluiTec.AppFx.Data.EntityNameServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Data.Sql.Tests
{
    /// <summary>
    /// A SQL builder test.
    /// </summary>
    [TestClass]
    public abstract class SqlBuilderTest
    {
        /// <summary>
        /// (Immutable) the builder.
        /// </summary>
        protected readonly ISqlBuilder Builder;
        
        /// <summary>
        /// Specialized default constructor for use only by derived class.
        /// </summary>
        protected SqlBuilderTest()
        {
            // ReSharper disable once VirtualMemberCallInConstructor
            Builder = GetConnection().GetBuilder(EntityNameService.GetDefault(), null);
        }

        /// <summary>
        /// Gets the connection.
        /// </summary>
        ///
        /// <returns>
        /// The connection.
        /// </returns>
        protected abstract IDbConnection GetConnection();
    }
}