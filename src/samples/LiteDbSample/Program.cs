using FluiTec.AppFx.Console.Helpers;
using FluiTec.AppFx.Data.LiteDb;
using LiteDbSample.Data;
using LiteDbSample.Data.Entities;

namespace LiteDbSample
{
    internal class Program
    {
        private static void Main()
        {
            var options = new LiteDbServiceOptions
            {
                DbFileName = "test.ldb",
                ApplicationFolder = DirectoryHelper.GetApplicationRoot(),
                UseSingletonConnection = true
            };

            using var service = new TestDbService(options, null);
            using var uow = service.BeginUnitOfWork();
            var unused = uow.DummyRepository.GetAll();
            uow.DummyRepository.Add(new DummyEntity {Name = "Test"});
            uow.Commit();
        }
    }
}