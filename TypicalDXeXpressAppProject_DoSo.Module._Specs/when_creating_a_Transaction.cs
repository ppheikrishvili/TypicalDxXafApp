using System;
using FsCheck.Xunit;
using Shouldly;
using TypicalDXeXpressAppProject_DoSo.Module.BusinessObjects;

// ReSharper disable InconsistentNaming

namespace TypicalDXeXpressAppProject_DoSo.Module._Specs
{
    public class When_creating_a_Transaction : XpoTestsBase
    {
        [Property]
        public void If_ServiceRateDiscount_DOES_exist_for_Customer_and_ServiceRate_it_should_be_assigned()
        {
            const int defaultRate = 15;
            const int adjustedRate = 10;

            // Arrange
            var customer = new Customer(Uow) { DateOfBirth = new DateTime(1990, 1, 1) };
            var serviceType = new ServiceType(Uow) { DefaultRate = defaultRate };
            var serviceRateDiscount = new ServiceRateDiscount(Uow) { ServiceType = serviceType, Customer = customer, AdjustedRate = adjustedRate };
            Uow.CommitChanges();

            // Act
            var transaction = new Transaction(Uow) { Customer = customer, ServiceType = serviceType };

            // Assert
            transaction.ServiceRateDiscount.ShouldBe(serviceRateDiscount);
            transaction.TransactionPrice.ShouldBe(adjustedRate);
        }

        [Property]
        public void If_ServiceRateDiscount_does_NOT_exist_for_Customer_and_ServiceRate_ServiceType_ServiceRate_should_be_applied()
        {
            const int defaultRate = 10;

            // Arrange
            var customer = new Customer(Uow) { DateOfBirth = new DateTime(1990, 1, 1) };
            var serviceType = new ServiceType(Uow) { DefaultRate = defaultRate };
            Uow.CommitChanges();

            // Act
            var transaction = new Transaction(Uow) { Customer = customer, ServiceType = serviceType };

            // Assert
            transaction.TransactionPrice.ShouldBe(defaultRate);
        }
    }
}