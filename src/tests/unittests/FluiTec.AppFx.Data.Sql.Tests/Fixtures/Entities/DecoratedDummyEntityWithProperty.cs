﻿using FluiTec.AppFx.Data.EntityNames;

namespace FluiTec.AppFx.Data.Sql.Tests.Fixtures.Entities;

[EntityName("Test", "Dummy")]
public class DecoratedDummyEntityWithProperty
{
    public int Id { get; set; }
}