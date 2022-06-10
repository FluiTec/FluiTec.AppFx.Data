using FluiTec.AppFx.Data.Ef;
using FluiTec.AppFx.Data.Ef.DataServices;
using FluiTec.AppFx.Data.TestLibrary.Entities;
using Microsoft.EntityFrameworkCore;

namespace FluiTec.AppFx.Data.TestLibrary.Contexts
{
    /// <summary>
    ///     A test database context.
    /// </summary>
    public class TestDbContext : DynamicDbContext
    {
        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="dataService">  Type of the SQL. </param>
        public TestDbContext(IEfDataService dataService) : base(dataService)
        {
        }

        /// <summary>
        ///     Override this method to further configure the model that was discovered by convention from
        ///     the entity types exposed in <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> properties
        ///     on your derived context. The resulting model may be cached and re-used for subsequent
        ///     instances of your derived context.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         If a model is explicitly set on the options for this context (via
        ///         <see
        ///             cref="M:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.UseModel(Microsoft.EntityFrameworkCore.Metadata.IModel)" />
        ///         )
        ///         then this method will not be run.
        ///     </para>
        ///     <para>
        ///         See
        ///         <see href="https://aka.ms/efcore-docs-modeling">
        ///             Modeling entity types and
        ///             relationships
        ///         </see>
        ///         for more information.
        ///     </para>
        /// </remarks>
        /// <param name="modelBuilder">
        ///     The builder being used to construct the model for this context.
        ///     Databases (and other extensions) typically define extension
        ///     methods on this object that allow you to configure aspects of the
        ///     model that are specific to a given database.
        /// </param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureModelForEntity<DummyEntity>(modelBuilder);
            ConfigureModelForEntity<Dummy2Entity>(modelBuilder);
            ConfigureModelForEntity<DateTimeDummyEntity>(modelBuilder);
        }
    }
}