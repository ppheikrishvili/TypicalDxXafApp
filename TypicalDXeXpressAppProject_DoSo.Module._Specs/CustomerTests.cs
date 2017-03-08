using System;
using FsCheck.Xunit;
using Shouldly;
using TypicalDXeXpressAppProject_DoSo.Module.BusinessObjects;
using Xunit;

namespace TypicalDXeXpressAppProject_DoSo.Module._Specs
{
    public class CustomerTests : XpoTestsBase
    {
        [Fact]
        public void When_creating_a_Customer_contract_should_get_created()
        {
            // Arrange & Act
            var customer = new Customer(Uow);
            Uow.CommitChanges();

            // Assert
            GetOne<Customer>(c => c.ID == customer.ID).Contracts.Count.ShouldBe(1);
        }

        [Fact]
        public void When_saving_an_existing_customer_new_Contract_should_NOT_be_added()
        {
            // Arrange
            var customer = new Customer(Uow);
            Uow.CommitChanges();

            // Act
            var anotherUow = GetUow();
            var existingCustomer = anotherUow.GetObjectByKey<Customer>(customer.ID);
            existingCustomer.DateOfBirth = new DateTime(1983, 1, 1);
            anotherUow.CommitChanges();

            // Assert
            GetReloaded(GetOne<Customer>(c => c.ID == customer.ID)).Contracts.Count.ShouldBe(1);
        }

        [Property(MaxTest = 500)]
        public void Age_calculation_should_be_accurate(DateTime dateOfBirth, DateTime currentDate)
        {
            // Act
            var age = dateOfBirth.GetAge(currentDate);

            // თუ ასაკის დათვლისას მოხდა შეცდომა, შედეგი დავიკიდოთ
            if (!age.IsSuccess) return;

            // Assert
            dateOfBirth.ShouldBeLessThanOrEqualTo(currentDate);
            age.Value.ShouldBeLessThan(120);

            var isBeforeBirthDay = dateOfBirth.AddYears(-dateOfBirth.Year + 1) > currentDate.AddYears(-currentDate.Year + 1);
            age.Value.ShouldBe((currentDate.Year - dateOfBirth.Year) - (isBeforeBirthDay ? 1 : 0));
        }
    }
}