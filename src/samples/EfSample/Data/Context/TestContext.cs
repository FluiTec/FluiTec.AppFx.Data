using EfSample.Data.Entities;
using FluiTec.AppFx.Data.Ef;
using FluiTec.AppFx.Data.Migration;
using Microsoft.EntityFrameworkCore;

namespace EfSample.Data.Context;

public class TestDbContext : DynamicDbContext
{
    public TestDbContext(SqlType sqlType, string connectionString) : base(sqlType, connectionString)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ConfigureModelForEntity<DummyEntity>(modelBuilder);
    }
}