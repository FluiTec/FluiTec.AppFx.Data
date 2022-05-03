using EfSample.Data.Entities;
using FluiTec.AppFx.Data.Ef;
using FluiTec.AppFx.Data.Ef.DataServices;
using Microsoft.EntityFrameworkCore;

namespace EfSample.Data.Context;

public class TestDbContext : DynamicDbContext
{
    public TestDbContext(IEfDataService dataService) : base(dataService)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ConfigureModelForEntity<DummyEntity>(modelBuilder);
    }
}