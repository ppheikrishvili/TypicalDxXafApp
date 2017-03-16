﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TypicalDXeXpressAppProject_DoSo.Module.BusinessObjects;

namespace TypicalDXeXpressAppProject_DoSo.Module._Specs.ClassLibrary
{
    public partial class StockTransaction : BaseClass, IBaseClass
    {
        public int Id
        {
            get { return GetPropertyValue( () => Id); }
            set { SetPropertyValue(() => Id, value); }
        }


        public DateTime Date
        {
            get { return GetPropertyValue( () => Date); }
            set { SetPropertyValue(() => Date, value); }
        }

        public Customer customer
        {
            get { return GetPropertyValue( () => customer); }
            set { SetPropertyValue(() => customer, value); }
        }

        public StockItem stockItem
        {
            get { return GetPropertyValue( () => stockItem); }
            set { SetPropertyValue(() => stockItem, value); }
        }


        public decimal Amount
        {
            get { return GetPropertyValue( () => Amount); }
            set { SetPropertyValue(() => Amount, value); }
        }


        public string TransactionNumber
        {
            get { return GetPropertyValue( () => TransactionNumber); }
            set { SetPropertyValue(() => TransactionNumber, value); }
        }
    }
}
