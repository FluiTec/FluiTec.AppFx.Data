using FluiTec.AppFx.Data.Sql.Attributes;

namespace FluiTec.AppFx.Data.Sql.Tests.Entities
{
    public class MultiKeyDummy
    {
        [SqlKey(false,0)]
        public int Id { get; set; }

        [SqlKey(false,1)]
        public int MyKey { get; set; }
    }
}