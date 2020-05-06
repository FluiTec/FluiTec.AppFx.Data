using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using FluiTec.AppFx.Data.LiteDb;
using LiteDbSample.Data;
using LiteDbSample.Data.Entities;

namespace LiteDbSample
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var options = new LiteDbServiceOptions
            {
                DbFileName = "test.ldb",
                ApplicationFolder = GetApplicationRoot(),
                UseSingletonConnection = true
            };

            using var service = new TestDbService(options, null);
            using var uow = service.BeginUnitOfWork();
            var existing = uow.DummyRepository.GetAll();
            uow.DummyRepository.Add(new DummyEntity {Name = "Test"});
            uow.Commit();
        }

        private static string GetApplicationRoot()
        {
            var exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);

            Debug.Assert(exePath != null);

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
}