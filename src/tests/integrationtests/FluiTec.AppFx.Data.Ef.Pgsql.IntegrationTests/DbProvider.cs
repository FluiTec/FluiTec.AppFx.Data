﻿using FluiTec.AppFx.Data.TestLibrary.DataServiceProviders;
using FluiTec.AppFx.Data.TestLibrary.DataServices;
using FluiTec.AppFx.Data.TestLibrary.UnitsOfWork;

namespace FluiTec.AppFx.Data.Ef.Pgsql.IntegrationTests;

/// <summary>
///     A database provider.
/// </summary>
internal class DbProvider : PgsqlEfDataServiceProvider<ITestDataService, ITestUnitOfWork>
{
    /// <summary>
    ///     Provide data service.
    /// </summary>
    /// <returns>
    ///     A TDataService.
    /// </returns>
    public override ITestDataService ProvideDataService()
    {
        return new EfTestDataService(ServiceOptions, null);
    }
}