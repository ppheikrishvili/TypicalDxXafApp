using System;

namespace TypicalDXeXpressAppProject_DoSo.Module.BusinessObjects
{
    public class Email
    {
        protected Email(string emailAddress)
        {
            if (emailAddress == null) throw new InvalidOperationException("Email address may not be null!");
            if (!emailAddress.Contains("@")) throw new InvalidOperationException("Email address may not be null!");
        }


    }
}