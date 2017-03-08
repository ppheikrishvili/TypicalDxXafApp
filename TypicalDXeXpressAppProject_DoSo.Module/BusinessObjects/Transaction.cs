using System.ComponentModel;
using CSharpFunctionalExtensions;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using static TypicalDXeXpressAppProject_DoSo.Module.BusinessObjects.TransactionMethods;

namespace TypicalDXeXpressAppProject_DoSo.Module.BusinessObjects
{
    [DefaultClassOptions]
    public class Transaction : XPLiteObjectBase
    {
        public Transaction(Session session) : base(session) { }

        [Association]
        public Customer Customer { get; set; }

        public ServiceType ServiceType { get; set; }

        public ServiceRateDiscount ServiceRateDiscount { get; set; }
        //public Result<ServiceRateDiscount> ServiceRateDiscountApplicationResult { get; private set; }
        //public string ServiceRateDiscountApplicationResultString => ServiceRateDiscountApplicationResult.ResultString();

        public decimal TransactionPrice { get; set; }
        [Browsable(false)]

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
            if (IsLoading) return;
            if (ServiceType == null || Customer == null) return;

            ValidateCustomer(Customer)
                .OnSuccess(() => GetSingleServiceRateDiscountResult(Session, ServiceType, Customer)
                    .OnSuccess(d => ValidateDiscount(d)
                        .OnSuccess(r =>
                        {
                            ServiceRateDiscount = r;
                            TransactionPrice = ServiceRateDiscount.AdjustedRate;
                        }))
                    .OnFailure(() =>
                    {
                        ServiceRateDiscount = null;
                        TransactionPrice = ServiceType?.DefaultRate ?? 0;
                    })
                );
        }
    }
}