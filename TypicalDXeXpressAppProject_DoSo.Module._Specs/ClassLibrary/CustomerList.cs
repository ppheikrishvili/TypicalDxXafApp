using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Xpo;
using Shouldly;
using TypicalDXeXpressAppProject_DoSo.Module.BusinessObjects;








namespace TypicalDXeXpressAppProject_DoSo.Module._Specs.ClassLibrary
{



   


    public class CustomerList : BaseClassList<Customer>
    {

        public CustomerList(Session sesssion) : base(sesssion)
            {

        }



        /// <summary>
        /// 
        /// </summary>
        public void FillData()
        {
            for (var i = 1; i < 10; i++)
            {
                this.Add(new Customer(Session) { ID = i, Name = "Customer - " + i.ToString(), DateOfBirth = new DateTime(1990, 01,21).AddMonths(i) });
            }
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="sesssion"></param>
        /// <returns></returns>
        public static CustomerList CreateNew(Session sesssion)
        {
            var tmpCustomerItemList = new CustomerList(sesssion);

            tmpCustomerItemList.FillData();

            return tmpCustomerItemList;

        }
    }
}
