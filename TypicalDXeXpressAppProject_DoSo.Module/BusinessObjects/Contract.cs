using DevExpress.Persistent.Base;
using DevExpress.Xpo;

namespace TypicalDXeXpressAppProject_DoSo.Module.BusinessObjects
{
    [DefaultClassOptions]
    public class Contract : XPLiteObjectBase
    {
        public Contract(Session session) : base(session) { }

        [Association]
        public Customer Customer { get; set; }
    }
}