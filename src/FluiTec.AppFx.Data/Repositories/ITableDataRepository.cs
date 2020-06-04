using FluiTec.AppFx.Data.Entities;

namespace FluiTec.AppFx.Data.Repositories
{
    /// <summary>Interface for a table based repository.</summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <seealso cref="FluiTec.AppFx.Data.Repositories.IDataRepository{TEntity}" />
    public interface ITableDataRepository<out TEntity> : IDataRepository<TEntity>
        where TEntity : class, IEntity, new()
    {
        /// <summary>	Gets the name of the table. </summary>
        /// <value>	The name of the table. </value>
        // ReSharper disable once UnusedMemberInSuper.Global
        string TableName { get; }
    }
}