using DevExpress.ExpressApp.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;

namespace TypicalDXeXpressAppProject_DoSo.Module.BusinessObjects
{
    [NonPersistent]
    public class XPLiteObjectBase : XPLiteObject
    {
        public XPLiteObjectBase(Session session) : base(session) { }
        public XPLiteObjectBase(Session session, bool raiseEventAfterConstructorCallEvent) : base(session) { }

        [Key(true)]
        public int ID { get; set; }
    }


    [DefaultClassOptions]
    public class CommercialUsers : XPLiteObjectBase
    {
        public CommercialUsers(Session session) : base(session) { }

        [Size(30)]
        [SearchMemberOptions(SearchMemberMode.Include)]
        public string Name { get; set; }
        [Size(20)]
        [SearchMemberOptions(SearchMemberMode.Include)]
        public string LoginName { get; set; }
        [Size(50)]
        public string Password { get; set; }
        public bool Status { get; set; }

        //[Association]
        //public XPCollection<Contract> Contracts => GetCollection<Contract>(nameof(Contracts));
        //[Association]
        //public XPCollection<Invoice> Invoices => GetCollection<Invoice>(nameof(Invoices));
        //[Association]
        //public XPCollection<Transaction> OfficeTransactionsCollection => GetCollection<Transaction>(nameof(TransactionsCollection));
    }
}
