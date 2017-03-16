using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Data.Helpers;
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




    /// <summary>
    /// 
    /// </summary>
    public partial class StockTransactionList : BaseClassList<StockTransaction>
    {


        public StockBalanceList GetBalanceList()
        {
            //var tmpStockBalanceList = new StockBalanceList();

            //List<StockItem> tmpStockItemsList = this.Select( S => S.stockItem).Distinct().ToList();

            //foreach (var tmpStockItem in tmpStockItemsList)
            //{
            //    var oneStockItemList = this.Where(W => W.stockItem.Id == tmpStockItem.Id).ToList();

            //    tmpStockBalanceList.Add( new StockBalance() { stockItem = tmpStockItem, Amount = oneStockItemList.Sum( SS => SS. ) });

            //    tmpStockBalanceList.AddRange(oneStockItemList.Select( S => 
                
            //    new StockBalance()
            //    {
            //        Id = S.Id,
            //        stockItem = S.stockItem,
            //        Amount = 
            //    }
            //    ));
            //}


            return new StockBalanceList();
        }
    }


}
