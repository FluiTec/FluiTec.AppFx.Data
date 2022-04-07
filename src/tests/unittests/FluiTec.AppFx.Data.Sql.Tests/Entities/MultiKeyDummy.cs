using FluiTec.AppFx.Data.Sql.Attributes;

namespace FluiTec.AppFx.Data.Sql.Tests.Entities
{
    public class MultiKeyDummy
    {
        [SqlKey(false)]
        public int Id { get; set; }

        [SqlKey(false)]
        public int MyKey { get; set; }
    }
}