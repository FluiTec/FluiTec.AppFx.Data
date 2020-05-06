using LiteDbSample.Data.Repositories;

namespace LiteDbSample.Data
{
    /// <summary>   Interface for test unit of work. </summary>
    public interface ITestUnitOfWork
    {
        DummyRepository DummyRepository { get; }
    }
}