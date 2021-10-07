﻿using Cli.InteractiveSample.Data.Schema;
using FluiTec.AppFx.Data.Entities;
using FluiTec.AppFx.Data.EntityNameServices;

namespace Cli.InteractiveSample.Data.Entities
{
    /// <summary>   A dummy entity. </summary>
    [EntityName(SchemaGlobals.Schema, SchemaGlobals.DummyTable)]
    public class DummyEntity : IKeyEntity<int>
    {
        /// <summary>   Gets or sets the name. </summary>
        /// <value> The name. </value>
        public string Name { get; set; }

        /// <summary>   Gets or sets the identifier. </summary>
        /// <value> The identifier. </value>
        public int Id { get; set; }
    }
}