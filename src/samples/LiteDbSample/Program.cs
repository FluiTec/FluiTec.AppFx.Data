using System;
using FluiTec.AppFx.Data.DataServices;
using FluiTec.AppFx.Data.UnitsOfWork;
using FluiTec.AppFx.Data.LiteDb;
using FluiTec.AppFx.Data.LiteDb.DataServices;
using FluiTec.AppFx.Data.LiteDb.UnitsOfWork;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace LiteDbSample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Test");

            var options = new LiteDbServiceOptions 
            { 
                DbFileName = "test.ldb",
                ApplicationFolder = GetApplicationRoot(),
                UseSingletonConnection = true
            };

            using (var service = new TestDbService(options, null, null))
            {
                using (var uow = service.BeginUnitOfWork())
                {

                }
            }
        }

        private static string GetApplicationRoot()
        {
            var exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) 
            {                
                var appPathMatcher = new Regex(@"(?<!fil)[A-Za-z]:\\+[\S\s]*?(?=\\+bin)");
                var appRoot = appPathMatcher.Match(exePath).Value;
                return appRoot;
            }
            else
            {
                var appPathMatcher = new Regex(@"(?<!file)\/+[\S\s]*?(?=\/+bin)");
                var appRoot = appPathMatcher.Match(exePath).Value;
                return appRoot;
            }
        }
    }

    public class TestDbService : LiteDbDataService
    {
        public override string Name => "TestDbService";

        public TestDbService(LiteDbServiceOptions options, ILogger<IDataService> logger, ILoggerFactory loggerFactory) : base(options, logger, loggerFactory)
        {

        }
    }

    public interface ITestUnitOfWork
    {

    }

    public class TestUnitOfWork : LiteDbUnitOfWork, ITestUnitOfWork
    {
        public TestUnitOfWork(LiteDbDataService service, ILogger<IUnitOfWork> logger) : base(service, logger)
        {

        }
    }
}
