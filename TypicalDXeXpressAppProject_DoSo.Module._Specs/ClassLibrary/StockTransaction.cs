using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Data.Helpers;
using TypicalDXeXpressAppProject_DoSo.Module.BusinessObjects;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;


namespace TypicalDXeXpressAppProject_DoSo.Module._Specs.ClassLibrary
{
    public partial class StockTransaction : BaseClass, IBaseClass
    {

        public StockTransaction() : base() { }

        //public StockTransaction(Session session) : base(session) { }


        public DateTime Date
        {
            get { return GetPropertyValue( () => Date); }
            set { SetPropertyValue(() => Date, value); }
        }

        public Customer customer
        {
            get { return GetPropertyValue(() => customer); }
            set { SetPropertyValue(() => customer, value); }
        }


        //public Customer customer { get;
        //    set;
        //}


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


        public XPCollection<StockBalance> StockBalances => GetCollection<StockBalance>(nameof(StockBalance));


        protected override void OnSaving()
        {
            base.OnSaving();

            //if (!StockBalances.Any())
            //    StockBalances.Add( new StockBalance(Session));

            if (!IsDeleted)
            {
                if (Amount < 0)
                {
                    throw new Exception("Negative amount!");
                }
            }

        }

    }




    /// <summary>
    /// 
    /// </summary>
    public partial class StockTransactionList : BaseClassList<StockTransaction>
    {
        private const string TranNumberPrefix = "ST";




        public StockTransactionList(Session sesssion) : base(sesssion)
        {
           
        }


        public StockTransactionList() : base()
        {
           
        }


        public void AddOne(StockTransaction stockTransaction)
        {
            string MaxAmount =
            (this.Where(W => ((W.customer.ID == stockTransaction.customer.ID) && !W.IsDeleted))
                 .Select(S => S.Amount)
                 .ToList()
                 .Sum() + 1).ToString();

            stockTransaction.TransactionNumber = TranNumberPrefix + stockTransaction.customer.ID.ToString() + "-" +
                                                 MaxAmount + "-" + DateTime.Now.ToString("yy");

            this.Add(stockTransaction);


        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public decimal CalculateBalanceForStockItem(StockItem item, DateTime date)
        {
            return this.Where(W =>( ( W.stockItem.ID == item.ID) && ( W.Date <= date ) && !W.IsDeleted ) ).Select( S => S.Amount).ToList().Sum();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public decimal CalculateBalanceForStockItemAfterDate(StockItem item, DateTime date)
        {
            return this.Where(W => ((W.stockItem.ID == item.ID) && (W.Date >= date) && !W.IsDeleted)).Select(S => S.Amount).ToList().Sum();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public StockBalanceList GetBalanceList()
        {
            var tmpStockBalanceList = new StockBalanceList();

            List<int> tmpStockItemsList = this.Select( S => S.stockItem.ID).Distinct().ToList();

            foreach (var tmpStockItemid in tmpStockItemsList)
            {
                var oneStockItemList = this.Where(W => W.stockItem.ID == tmpStockItemid).ToList();

                tmpStockBalanceList.Add(new StockBalance() { Amount = oneStockItemList.Select(S => S.Amount).Sum(), stockItem = oneStockItemList.Select(ss=> ss.stockItem).FirstOrDefault() });
            }


            return tmpStockBalanceList;
        }
    }


}
