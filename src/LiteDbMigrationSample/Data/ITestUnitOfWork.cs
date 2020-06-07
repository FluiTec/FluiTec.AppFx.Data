using LiteDbMigrationSample.Data.Repositories;

namespace LiteDbMigrationSample.Data
{
    /// <summary>   Interface for test unit of work. </summary>
    public interface ITestUnitOfWork
    {
        // ReSharper disable once UnusedMemberInSuper.Global
        DummyRepository DummyRepository { get; }
    }
}