﻿using FluiTec.AppFx.Data.EntityNames;
using FluiTec.AppFx.Data.Reflection;

namespace FluiTec.AppFx.Data.Sql.Tests.Fixtures.Entities;

[EntityName("Test", "Dummy")]
public class DecoratedDummyEntityWithProperty
{
    [EntityKey] public int Id { get; set; }
}