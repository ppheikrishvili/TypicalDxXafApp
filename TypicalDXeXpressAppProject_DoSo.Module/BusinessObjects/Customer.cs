using System;
using System.Linq;
using CSharpFunctionalExtensions;
using DevExpress.ExpressApp.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System.ComponentModel;
using static TypicalDXeXpressAppProject_DoSo.Module.BusinessObjects.TransactionMethods;

namespace TypicalDXeXpressAppProject_DoSo.Module.BusinessObjects
{
    [DefaultClassOptions]
    public class Customer : XPLiteObjectBase
    {
        public Customer(Session session) : base(session) { }

        public DateTime DateOfBirth { get; set; }
        [Browsable(false)]
        public Result<int> AgeCalculationResult => DateOfBirth.GetAge(DateTime.Now);
        public string AgeCalculationResultString => AgeCalculationResult.ResultString();
        public int Age => AgeCalculationResult.IsSuccess ? AgeCalculationResult.Value : -1;

        public bool IsDeactivated { get; set; }

        public enum CustomerStatusEnum
        {
            Regular,
            Forwarder,
            Reinsurer
        }

        [Size(100)]
        [SearchMemberOptions(SearchMemberMode.Include), RuleRequiredField]
        public string Name { get; set; }

        public string Email { get; set; }

        public CustomerStatusEnum? CustomerStatus { get; set; }

        [Association]
        public XPCollection<Contract> Contracts => GetCollection<Contract>(nameof(Contracts));
        [Association]
        public XPCollection<Transaction> Transactions => GetCollection<Transaction>(nameof(Transactions));
        [Association]
        public XPCollection<Invoice> Invoices => GetCollection<Invoice>(nameof(Invoices));

        protected override void OnSaving()
        {
            base.OnSaving();

            if (!Contracts.Any())
                Contracts.Add(new Contract(Session));
        }
    }

    [DefaultClassOptions]
    public class Contract : XPLiteObjectBase
    {
        public Contract(Session session) : base(session) { }

        [Association]
        public Customer Customer { get; set; }

        //[Association]
        //public Customer Customer { get; set; }
    }

    [DefaultClassOptions]
    public class Transaction : XPLiteObjectBase
    {
        public Transaction(Session session) : base(session) { }

        [Association]
        public Customer Customer { get; set; }

        public ServiceType ServiceType { get; set; }

        public ServiceRateDiscount ServiceRateDiscount { get; set; }

        public decimal TransactionPrice { get; set; }

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
            if (IsLoading) return;

            GetSingleServiceRateDiscountResult(Session, ServiceType, Customer)
                .OnSuccess(i => new { DiscountResult = i, CustomerValidationResult = ValidateCustomer(i.Customer) })
                .OnSuccess(d => new { d.DiscountResult, ValidatedDiscount = ValidateDiscount(d.DiscountResult) })
                .OnSuccess(r =>
                {
                    ServiceRateDiscount = r.DiscountResult;
                    TransactionPrice = ServiceRateDiscount.AdjustedRate;
                })
                .OnFailure(() =>
                {
                    ServiceRateDiscount = null;
                    TransactionPrice = ServiceType?.DefaultRate ?? 0;
                });
        }
    }

    [DefaultClassOptions]
    public class Invoice : XPLiteObjectBase
    {
        public Invoice(Session session) : base(session) { }

        [Association]
        public Customer Customer { get; set; }
    }
}
