using DevExpress.ExpressApp.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;

namespace TypicalDXeXpressAppProject_DoSo.Module.BusinessObjects
{
    [DefaultClassOptions]
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
        [SearchMemberOptions(SearchMemberMode.Include)]
        public string Name { get; set; }
        
        public CustomerStatusEnum CustomerStatus { get; set; }

        //[Association]
        //public XPCollection<Contract> Contracts => GetCollection<Contract>(nameof(Contracts));
        //[Association]
        //public XPCollection<Invoice> Invoices => GetCollection<Invoice>(nameof(Invoices));
        //[Association]
        //public XPCollection<Transaction> OfficeTransactionsCollection => GetCollection<Transaction>(nameof(TransactionsCollection));
    }
}
