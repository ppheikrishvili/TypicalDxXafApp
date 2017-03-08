using DevExpress.Persistent.Base;
using DevExpress.Xpo;

namespace TypicalDXeXpressAppProject_DoSo.Module.BusinessObjects
{
    [DefaultClassOptions]
    public class Transaction : XPLiteObjectBase
    {
        public Transaction(Session session) : base(session) { }

        [Association]
        public Customer Customer { get; set; }
    }
}