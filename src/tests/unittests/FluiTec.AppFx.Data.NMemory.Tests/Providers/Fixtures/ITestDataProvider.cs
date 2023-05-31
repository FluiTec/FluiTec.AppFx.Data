using FluiTec.AppFx.Data.DataProviders;

namespace FluiTec.AppFx.Data.NMemory.Tests.Providers.Fixtures;

public interface ITestDataProvider : IDataProvider<ITestDataService, ITestUnitOfWork> { }