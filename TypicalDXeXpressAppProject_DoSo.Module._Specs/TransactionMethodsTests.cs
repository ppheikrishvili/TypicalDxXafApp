using System;
using Shouldly;
using TypicalDXeXpressAppProject_DoSo.Module.BusinessObjects;
using Xunit;

namespace TypicalDXeXpressAppProject_DoSo.Module._Specs
{
    public class TransactionMethodsTests : XpoTestsBase
    {
        [Fact]
        public void When_GetSingleServiceRateDiscountResult_and_discount_does_NOT_exist()
        {
            const int defaultRate = 15;

            // Arrange
            var customer = new Customer(Uow) { DateOfBirth = new DateTime(1990, 1, 1) };
            var serviceType = new ServiceType(Uow) { DefaultRate = defaultRate };
            Uow.CommitChanges();

            // Assert
            TransactionMethods.GetSingleServiceRateDiscountResult(Uow, serviceType, customer).IsFailure.ShouldBeTrue();
        }
        [Fact]
        public void When_GetSingleServiceRateDiscountResult_and_discount_DOES_exist_it_should_return_it()
        {
            const int defaultRate = 15;
            const int adjustedRate = 10;

            // Arrange
            var customer = new Customer(Uow) { DateOfBirth = new DateTime(1990, 1, 1) };
            var serviceType = new ServiceType(Uow) { DefaultRate = defaultRate };
            var serviceRateDiscount = new ServiceRateDiscount(Uow) { ServiceType = serviceType, Customer = customer, AdjustedRate = adjustedRate };
            Uow.CommitChanges();

            // Assert
            TransactionMethods.GetSingleServiceRateDiscountResult(Uow, serviceType, customer).Value.ShouldBe(serviceRateDiscount);
        }
        [Fact]
        public void When_GetSingleServiceRateDiscountResult_and_more_than_1_discount_DOES_exist_it_should_return_none()
        {
            const int defaultRate = 15;
            const int adjustedRate = 10;

            // Arrange
            var customer = new Customer(Uow) { DateOfBirth = new DateTime(1990, 1, 1) };
            var serviceType = new ServiceType(Uow) { DefaultRate = defaultRate };
            // ReSharper disable once ObjectCreationAsStatement
            new ServiceRateDiscount(Uow) { ServiceType = serviceType, Customer = customer, AdjustedRate = adjustedRate };
            // ReSharper disable once ObjectCreationAsStatement
            new ServiceRateDiscount(Uow) { ServiceType = serviceType, Customer = customer, AdjustedRate = adjustedRate };
            Uow.CommitChanges();

            // Assert
            TransactionMethods.GetSingleServiceRateDiscountResult(Uow, serviceType, customer).IsFailure.ShouldBeTrue();
        }

    }
}