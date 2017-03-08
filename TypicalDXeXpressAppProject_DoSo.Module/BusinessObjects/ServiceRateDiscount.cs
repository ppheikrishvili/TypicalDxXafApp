using DevExpress.Persistent.Base;
using DevExpress.Xpo;

namespace TypicalDXeXpressAppProject_DoSo.Module.BusinessObjects
{
    [DefaultClassOptions]
    public class ServiceRateDiscount : XPLiteObjectBase
    {
        public ServiceRateDiscount(Session session) : base(session) { }

        public ServiceType ServiceType { get; set; }
        public Customer Customer { get; set; }

        public decimal AdjustedRate { get; set; }

        protected override void OnSaving()
        {
            base.OnSaving();
        }
    }
}