using DynamicSample.Data.LiteDb;
using FluiTec.AppFx.Data.Dapper.Mssql;
using FluiTec.AppFx.Data.Dapper.Mysql;
using FluiTec.AppFx.Data.Dapper.Pgsql;
using FluiTec.AppFx.Data.Dapper.SqLite;
using FluiTec.AppFx.Data.Dynamic;
using FluiTec.AppFx.Data.Dynamic.Configuration;
using FluiTec.AppFx.Data.LiteDb;

namespace DynamicSample.Data
{
    public class DynamicTestDataProvider : DynamicDataProvider<ITestDataService>
    {
        public DynamicTestDataProvider(DynamicDataOptions options) : base(options)
        {
        }

        protected override ITestDataService ProvideUsingMssql(MssqlDapperServiceOptions options)
        {
            throw new System.NotImplementedException();
        }

        protected override ITestDataService ProvideUsingMysql(MysqlDapperServiceOptions options)
        {
            throw new System.NotImplementedException();
        }

        protected override ITestDataService ProvideUsingPgsql(PgsqlDapperServiceOptions options)
        {
            throw new System.NotImplementedException();
        }

        protected override ITestDataService ProvideUsingSqlite(SqliteDapperServiceOptions options)
        {
            throw new System.NotImplementedException();
        }

        protected override ITestDataService ProvideUsingLiteDb(LiteDbServiceOptions options)
        {
            throw new System.NotImplementedException();
        }
    }
}