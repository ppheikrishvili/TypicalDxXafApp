using System;
using FsCheck.Xunit;
using Shouldly;
using TypicalDXeXpressAppProject_DoSo.Module.BusinessObjects;

namespace TypicalDXeXpressAppProject_DoSo.Module._Specs
{
    public class CustomerTests : XpoTestsBase
    {
        [Property]
        public void when_creating_a_Customer_contract_should_get_created()
        {
            // Arrange
            // Act
            var customer = new Customer(Uow);
            Uow.CommitChanges();

            // Assert
            GetOne<Customer>(c => c.ID == customer.ID).Contracts.Count.ShouldBe(1);
        }

        [Property(MaxTest = 500)]
        public void age_calculation_should_be_accurate(DateTime dateOfBirth, DateTime currentDate)
        {
            // Act
            var age = dateOfBirth.GetAge(currentDate);

            // თუ ასაკის დათვლისას მოხდა შეცდომა, გავაგრძელოთ
            if (!age.IsSuccess) return;

            // Assert
            dateOfBirth.ShouldBeLessThanOrEqualTo(currentDate);
            age.Value.ShouldBeLessThan(120);

            var isBeforeBirthDay = dateOfBirth.AddYears(-dateOfBirth.Year + 1) > currentDate.AddYears(-currentDate.Year + 1);

            age.Value.ShouldBe(+(currentDate.Year - dateOfBirth.Year) - (isBeforeBirthDay ? 1 : 0));
        }


    }

    public class when_creating_a_Transaction : XpoTestsBase
    {
        [Property]
        public void if_ServiceRateDiscount_exists_for_Customer_and_ServiceRate_it_should_be_assigned()
        {
            const int adjustedRate = 10;

            // Arrange
            var customer = new Customer(Uow);
            var serviceType = new ServiceType(Uow);
            var serviceRateDiscount = new ServiceRateDiscount(Uow) { ServiceType = serviceType, Customer = customer, AdjustedRate = adjustedRate };
            Uow.CommitChanges();

            // Act
            var transaction = new Transaction(Uow) { Customer = customer, ServiceType = serviceType };

            // Assert
            transaction.ServiceRateDiscount.ShouldBe(serviceRateDiscount);
            transaction.TransactionPrice.ShouldBe(adjustedRate);
        }

        [Property]
        public void if_ServiceRateDiscount_does_NOT_exist_for_Customer_and_ServiceRate_ServiceType_ServiceRate_should_be_applied()
        {
            const int defaultRate = 10;

            // Arrange
            var customer = new Customer(Uow);
            var serviceType = new ServiceType(Uow) { DefaultRate = defaultRate };
            //var serviceRateDiscount = new ServiceRateDiscount(Uow) { ServiceType = serviceType, Customer = customer, AdjustedRate = adjustedRate };
            Uow.CommitChanges();

            // Act
            var transaction = new Transaction(Uow) { Customer = customer, ServiceType = serviceType };

            // Assert
            transaction.TransactionPrice.ShouldBe(defaultRate);
        }
    }
}