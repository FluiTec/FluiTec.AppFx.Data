using System;
using Cli.InteractiveSample.Data.Schema;
using FluiTec.AppFx.Data.Entities;
using FluiTec.AppFx.Data.EntityNameServices;

namespace Cli.InteractiveSample.Data.Entities
{
    /// <summary>   A dummy entity 2. </summary>
    [EntityName(SchemaGlobals.Schema, SchemaGlobals.DummyTable2)]
    public class DummyEntity2 : IKeyEntity<int>, ITimeStampedKeyEntity
    {
        /// <summary>   Gets or sets the name. </summary>
        /// <value> The name. </value>
        public string Name { get; set; }

        /// <summary>   Gets or sets the identifier. </summary>
        /// <value> The identifier. </value>
        public int Id { get; set; }

        /// <summary>   Gets or sets the timestamp. </summary>
        /// <value> The timestamp. </value>
        public DateTimeOffset TimeStamp { get; set; }
    }
}