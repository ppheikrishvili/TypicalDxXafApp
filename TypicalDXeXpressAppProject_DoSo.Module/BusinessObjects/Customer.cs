using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
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
        [SearchMemberOptions(SearchMemberMode.Include), RuleRequiredField]
        public string Name { get; set; }

        public CustomerStatusEnum? CustomerStatus { get; set; }

        [Association]
        public XPCollection<Contract> Contracts => GetCollection<Contract>(nameof(Contracts));
        [Association]
        public XPCollection<Transaction> Transactions => GetCollection<Transaction>(nameof(Transactions));
        [Association]
        public XPCollection<Invoice> Invoices => GetCollection<Invoice>(nameof(Invoices));
    }

    [DefaultClassOptions]
    public class Contract : XPLiteObjectBase
    {
        public Contract(Session session) : base(session) { }

        [Association]
        public Customer Customer { get; set; }
    }

    [DefaultClassOptions]
    public class Transaction : XPLiteObjectBase
    {
        public Transaction(Session session) : base(session) { }

        [Association]
        public Customer Customer { get; set; }
    }

    [DefaultClassOptions]
    public class Invoice : XPLiteObjectBase
    {
        public Invoice(Session session) : base(session) { }

        [Association]
        public Customer Customer { get; set; }
    }
}
