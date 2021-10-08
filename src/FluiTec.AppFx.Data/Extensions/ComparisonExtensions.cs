using System;

namespace FluiTec.AppFx.Data.Extensions
{
    /// <summary>
    /// A comparison extensions.
    /// </summary>
    public static class ComparisonExtensions
    {
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
            return offset1.UtcTicks / 100000000 == offset2.UtcTicks / 100000000;
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
        public static bool EqualsRemovingPrecision(this DateTimeOffset? offset1, DateTimeOffset? offset2)
        {
            return offset1?.UtcTicks / 100000000 == offset2?.UtcTicks / 100000000;
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
        public static bool EqualsRemovingPrecision(this DateTimeOffset offset1, DateTimeOffset? offset2)
        {
            return offset1.UtcTicks / 100000000 == offset2?.UtcTicks / 100000000;
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
        public static bool EqualsRemovingPrecision(this DateTimeOffset? offset1, DateTimeOffset offset2)
        {
            return offset1?.UtcTicks / 100000000 == offset2.UtcTicks / 100000000;
        }
    }
}