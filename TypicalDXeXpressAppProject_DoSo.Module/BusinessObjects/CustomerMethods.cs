using System;
using CSharpFunctionalExtensions;

namespace TypicalDXeXpressAppProject_DoSo.Module.BusinessObjects
{
    public static class CustomerMethods
    {
        public const string DateOfBirthIsGtCurrentDate = "Date Of Birth may not be greater than Current Date";
        public const string YearDifferenceGt120 = "Difference between DOB and CurrentDate years may not exceed 120";
        public static Result<int> GetAge(this DateTime dateOfBirth, DateTime currentDate)
        {
            if (dateOfBirth > currentDate) return Result.Fail<int>(DateOfBirthIsGtCurrentDate);
            if (currentDate.Year - dateOfBirth.Year >= 120) return Result.Fail<int>(YearDifferenceGt120);

            var age = currentDate.Year - dateOfBirth.Year;
            if (dateOfBirth > currentDate.AddYears(-age)) age--;
            return Result.Ok(age);
        }
    }
}