using FluentMigrator;

namespace FluiTec.AppFx.Data.Dapper.Migration
{
    /// <summary>   Attribute for dapper migration. </summary>
    public class DapperMigrationAttribute : MigrationAttribute
    {
        /// <summary>   Constructor. </summary>
        /// <param name="year">     The year. </param>
        /// <param name="month">    The month. </param>
        /// <param name="day">      The day. </param>
        /// <param name="hour">     The hour. </param>
        /// <param name="minute">   The minute. </param>
        /// <param name="author">   The author. </param>
        public DapperMigrationAttribute(int year, int month, int day, int hour, int minute, string author)
            : base(CalculateValue(year, month, day, hour, minute))
        {
            Author = author;
        }

        /// <summary>   Gets the author. </summary>
        /// <value> The author. </value>
        public string Author { get; }

        /// <summary>   Calculates the value. </summary>
        /// <param name="year">     The year. </param>
        /// <param name="month">    The month. </param>
        /// <param name="day">      The day. </param>
        /// <param name="hour">     The hour. </param>
        /// <param name="minute">   The minute. </param>
        /// <returns>   The calculated value. </returns>
        private static long CalculateValue(int year, int month, int day, int hour, int minute)
        {
            return year * 100000000L + month * 1000000L + day * 10000L + hour * 100L + minute;
        }
    }
}