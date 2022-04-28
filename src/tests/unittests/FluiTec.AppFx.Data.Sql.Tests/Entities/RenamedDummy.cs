using FluiTec.AppFx.Data.Sql.Attributes;

namespace FluiTec.AppFx.Data.Sql.Tests.Entities
{
    public class RenamedDummy
    {
        /// <summary>
        ///     Gets or sets the identifier.
        /// </summary>
        /// <value>
        ///     The u identifier.
        /// </value>
        [SqlKey]
        public int UId { get; set; }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        /// <value>
        ///     The name.
        /// </value>
        public string Name { get; set; }
    }
}