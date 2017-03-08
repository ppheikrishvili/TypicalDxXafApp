using System;
using CSharpFunctionalExtensions;

namespace TypicalDXeXpressAppProject_DoSo.Module.BusinessObjects
{
    public static class ResultExtensions
    {
        public static string ResultString<T>(this Result<T> result)
        {
            try { return result.IsSuccess ? $"Success: [{result.Value}]" : result.Error; }
            catch { return "Exception happens.."; }
        }
    }
}