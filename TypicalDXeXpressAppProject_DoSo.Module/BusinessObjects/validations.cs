using System;
using CSharpFunctionalExtensions;
using DevExpress.Xpo;
using System.Linq;

namespace TypicalDXeXpressAppProject_DoSo.Module.BusinessObjects
{
    public class TransactionMethods
    {
        public static Result<ServiceRateDiscount> GetSingleServiceRateDiscountResult(Session session, ServiceType serviceType, Customer customer)
        {
            if (serviceType == null) return Result.Fail<ServiceRateDiscount>("serviceType may not be null");
            if (customer == null) return Result.Fail<ServiceRateDiscount>("customer may not be null");

            var serviceRateDiscountsForServiceType = session.Query<ServiceRateDiscount>()
                .Where(s => s.ServiceType == serviceType && s.Customer == customer).ToList();

            // ReSharper disable once ConvertIfStatementToSwitchStatement
            if (serviceRateDiscountsForServiceType.Count == 1)
                return Result.Ok(serviceRateDiscountsForServiceType.Single());
            if (serviceRateDiscountsForServiceType.Count == 0)
                return Result.Fail<ServiceRateDiscount>("Zero Discounts found");
            // ReSharper disable once ConvertIfStatementToReturnStatement
            if (serviceRateDiscountsForServiceType.Count > 1)
                return Result.Fail<ServiceRateDiscount>("Over 1 Discounts found!");

            return Result.Fail<ServiceRateDiscount>("this place should never have been reached");
        }

        public static Result<decimal> ValidateDiscount(ServiceRateDiscount discount)
        {
            if (discount.ServiceType == null) return Result.Fail<decimal>("ServiceType may not be null!");
            if (discount.Customer == null) return Result.Fail<decimal>("Customer may not be null!");
            // ReSharper disable once ConvertIfStatementToReturnStatement
            if (discount.AdjustedRate > discount.ServiceType.DefaultRate) return Result.Fail<decimal>("Adjusted rate may not be greater than Default Rate");

            return Result.Ok(discount.AdjustedRate);
        }

        public static Result ValidateCustomer(Customer customer)
        {
            if (customer.AgeCalculationResult.IsFailure) return customer.AgeCalculationResult;
            // ReSharper disable once ConvertIfStatementToReturnStatement
            if (customer.IsDeactivated) return Result.Fail("Customer is Deactivated!");

            return Result.Ok();
        }
    }
}