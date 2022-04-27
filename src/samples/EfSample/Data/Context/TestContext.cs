using EfSample.Data.Entities;
using FluiTec.AppFx.Data.Ef;
using FluiTec.AppFx.Data.Migration;
using Microsoft.EntityFrameworkCore;
using FluiTec.AppFx.Data.Ef.Extensions;

namespace EfSample.Data.Context;

public class TestDbContext : DynamicDbContext
{
    public TestDbContext(SqlType sqlType, string connectionString) : base(sqlType, connectionString)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var nameService = SqlBuilder.Adapter.GetNameService();

        modelBuilder.Entity<DummyEntity>()
            .ToTable(nameService);
        modelBuilder.Entity<DummyEntity>()
            .HasKey(nameof(DummyEntity.Id));

        modelBuilder.Entity<DummyEntity>()
            .Property(entity => entity.Id)
            .UseIdentityColumn();
        modelBuilder.Entity<DummyEntity>()
            .Property(entity => entity.Name);
    }
}