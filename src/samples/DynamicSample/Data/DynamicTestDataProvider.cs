using DynamicSample.Data.LiteDb;
using FluiTec.AppFx.Data.Dapper.Mssql;
using FluiTec.AppFx.Data.Dapper.Mysql;
using FluiTec.AppFx.Data.Dapper.Pgsql;
using FluiTec.AppFx.Data.Dapper.SqLite;
using FluiTec.AppFx.Data.Dynamic;
using FluiTec.AppFx.Data.Dynamic.Configuration;
using FluiTec.AppFx.Data.LiteDb;
using Microsoft.Extensions.Logging;

namespace DynamicSample.Data
{
    public class DynamicTestDataProvider : DynamicDataProvider<ITestDataService>
    {
        public DynamicTestDataProvider(DynamicDataOptions options) : base(options)
        {
        }

        protected override ITestDataService ProvideUsingMssql(MssqlDapperServiceOptions options, ILoggerFactory loggerFactory)
        {
            throw new System.NotImplementedException();
        }

        protected override ITestDataService ProvideUsingMysql(MysqlDapperServiceOptions options, ILoggerFactory loggerFactory)
        {
            throw new System.NotImplementedException();
        }

        protected override ITestDataService ProvideUsingPgsql(PgsqlDapperServiceOptions options, ILoggerFactory loggerFactory)
        {
            throw new System.NotImplementedException();
        }

        protected override ITestDataService ProvideUsingSqlite(SqliteDapperServiceOptions options, ILoggerFactory loggerFactory)
        {
            throw new System.NotImplementedException();
        }

        protected override ITestDataService ProvideUsingLiteDb(LiteDbServiceOptions options, ILoggerFactory loggerFactory)
        {
            throw new System.NotImplementedException();
        }
    }
}