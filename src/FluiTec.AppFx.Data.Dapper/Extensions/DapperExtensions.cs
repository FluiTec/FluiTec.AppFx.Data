using System;
using System.Threading;
using Dapper;
using FluiTec.AppFx.Data.Migration;

namespace FluiTec.AppFx.Data.Dapper.Extensions
{
    /// <summary>   A dapper extensions. </summary>
    public static class DapperExtensions
    {
        /// <summary>   The date time offset mapper installed. </summary>
        private static int _dateTimeOffsetMapperInstalled;

        /// <summary>   Installs the date time offset mapper. </summary>
        public static void InstallDateTimeOffsetMapper()
        {
            // Assumes SqlMapper.ResetTypeHandlers() is never called.
            if (Interlocked.CompareExchange(ref _dateTimeOffsetMapperInstalled, 1, 0) != 0) return;

            // First remove the default type map between typeof(DateTimeOffset) => DbType.DateTimeOffset (not valid for MySQL)
            SqlMapper.RemoveTypeMap(typeof(DateTimeOffset));
            SqlMapper.RemoveTypeMap(typeof(DateTimeOffset?));

            // This handles nullable value types automatically e.g. DateTimeOffset?
            SqlMapper.AddTypeHandler(typeof(DateTimeOffset), new DateTimeOffsetTypeHandler());
        }

        /// <summary>
        /// Needs date time mapping.
        /// </summary>
        ///
        /// <exception cref="ArgumentOutOfRangeException">  Thrown when one or more arguments are outside
        ///                                                 the required range. </exception>
        ///
        /// <param name="sqlType">  Type of the SQL. </param>
        ///
        /// <returns>
        /// True if it succeeds, false if it fails.
        /// </returns>
        public static bool NeedsDateTimeMapping(this SqlType sqlType)
        {
            return sqlType switch
            {
                SqlType.Mssql => false,
                SqlType.Mysql => true,
                SqlType.Pgsql => false,
                SqlType.Sqlite => true,
                _ => throw new ArgumentOutOfRangeException(nameof(sqlType), sqlType, null)
            };
        }

        /// <summary>
        /// Equals removing precision.
        /// </summary>
        ///
        /// <param name="offset1">  The first offset. </param>
        /// <param name="offset2">  The second offset. </param>
        ///
        /// <returns>
        /// True if equals removing precision, false if not.
        /// </returns>
        public static bool EqualsRemovingPrecision(this DateTimeOffset offset1, DateTimeOffset offset2)
        {
            return offset1.UtcTicks / 10000000 == offset2.UtcTicks / 10000000;;
        }
    }
}