using System;
using System.Collections.Generic;
using System.Text;
using FluiTec.AppFx.Data.Dapper.DataServices;
using FluiTec.AppFx.Data.DataServices;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Data.Dapper.Mssql
{
    public class MssqlDapperDataService : DapperDataService
    {
        public MssqlDapperDataService(IDapperServiceOptions dapperServiceOptions, ILogger<IDataService> logger, ILoggerFactory loggerFactory) : base(dapperServiceOptions, logger, loggerFactory)
        {
        }

        public override string Name { get; }
    }
}
