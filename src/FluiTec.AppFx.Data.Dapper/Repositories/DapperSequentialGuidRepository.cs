using System;
using FluiTec.AppFx.Data.Dapper.UnitsOfWork;
using FluiTec.AppFx.Data.Entities;
using FluiTec.AppFx.Data.Repositories;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Data.Dapper.Repositories
{
    /// <summary>	A dapper unique identifier repository. </summary>
    // ReSharper disable once UnusedMember.Global
    public abstract class DapperSequentialGuidRepository<TEntity> : DapperPreDefinedKeyRepository<TEntity, Guid>
        where TEntity : class, IKeyEntity<Guid>, new()
    {
        #region Constructors

        /// <summary>   Specialized constructor for use only by derived class. </summary>
        /// <param name="unitOfWork">   The unit of work. </param>
        /// <param name="logger">       The logger. </param>
        protected DapperSequentialGuidRepository(DapperUnitOfWork unitOfWork, ILogger<IRepository> logger) : base(
            unitOfWork, logger)
        {
        }

        #endregion

        #region DapperPredefinedKeyRepository

        /// <summary>	Sets insert key. </summary>
        /// <param name="entity">	The entity. </param>
        protected override void SetInsertKey(TEntity entity)
        {
            entity.Id = GuidHelper.SequentialGuid();
        }

        #endregion

        #region Helpers

        /// <summary>	A unique identifier helper. </summary>
        private static class GuidHelper
        {
            /// <summary>
            ///     Generates a guid based on the current date/time
            ///     http://stackoverflow.com/questions/1752004/sequential-guid-generator-c-sharp
            /// </summary>
            /// <returns></returns>
            public static Guid SequentialGuid()
            {
                var tempGuid = Guid.NewGuid();
                var bytes = tempGuid.ToByteArray();
                var time = DateTime.Now;
                bytes[3] = (byte) time.Year;
                bytes[2] = (byte) time.Month;
                bytes[1] = (byte) time.Day;
                bytes[0] = (byte) time.Hour;
                bytes[5] = (byte) time.Minute;
                bytes[4] = (byte) time.Second;
                return new Guid(bytes);
            }
        }

        #endregion
    }
}