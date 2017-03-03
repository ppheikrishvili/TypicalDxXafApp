using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;

namespace TypicalDXeXpressAppProject_DoSo.Module.BusinessObjects
{
    [DefaultClassOptions]
    [DefaultListViewOptions(MasterDetailMode.ListViewAndDetailView, true, NewItemRowPosition.Bottom)]
    public class Customer : XPLiteObjectBase
    {
        public Customer(Session session) : base(session) { }

        public enum CustomerStatusEnum
        {
            Regular,
            Forwarder,
            Reinsurer
        }

        [Size(100)]
        [SearchMemberOptions(SearchMemberMode.Include), RuleRequiredField]
        public string Name { get; set; }
        
        public CustomerStatusEnum? CustomerStatus { get; set; }
    }
}
