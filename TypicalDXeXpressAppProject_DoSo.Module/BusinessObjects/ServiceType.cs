using DevExpress.Persistent.Base;
using DevExpress.Xpo;

namespace TypicalDXeXpressAppProject_DoSo.Module.BusinessObjects
{
    [DefaultClassOptions]
    public class ServiceType : XPLiteObjectBase
    {
        public ServiceType(Session session) : base(session) { }

        [Association]
        public Customer Customer { get; set; }

        public decimal DefaultRate { get; set; }
    }
}