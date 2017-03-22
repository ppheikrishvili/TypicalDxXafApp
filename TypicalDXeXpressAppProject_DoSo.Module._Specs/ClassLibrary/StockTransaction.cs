using System;
using System.Collections.Generic;
using System.Linq;
using TypicalDXeXpressAppProject_DoSo.Module.BusinessObjects;
using DevExpress.Xpo;


namespace TypicalDXeXpressAppProject_DoSo.Module._Specs.ClassLibrary
{
    public class StockTransaction : BaseClass, IBaseClass
    {

        //public StockTransaction(Uow) : base() { }

        public StockTransaction(Session session) : base(session) { }


        public DateTime Date
        {
            get { return GetPropertyValue(() => Date); }
            set { SetPropertyValue(() => Date, value); }
        }

        public Customer customer
        {
            //get { return GetPropertyValue(() => customer); }
            get { return GetPropertyValue(() => customer); }
            set { SetPropertyValue(() => customer, value); }
        }


        //public Customer customer { get;
        //    set;
        //}


        public StockItem stockItem
        {
            get { return GetPropertyValue(() => stockItem); }
            set { SetPropertyValue(() => stockItem, value); }
        }


        public decimal Amount
        {
            get { return GetPropertyValue(() => Amount); }
            set { SetPropertyValue(() => Amount, value); }
        }


        public string TransactionNumber
        {
            get { return GetPropertyValue(() => TransactionNumber); }
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


        public enum FillDirection { Afterdate, Beforedate }


        public StockTransactionList(Session sesssion) : base(sesssion)
        {

        }


        //public StockTransactionList() : base()
        //{

        //}



        public string GetTransactionNumber(int customeriD)
        {
            var maxAmount =
            (this.Count(W => W.customer.ID == customeriD && W.Date.Year == DateTime.Now.Year && !W.IsDeleted) + 1).ToString();

            var transactionNumber = TranNumberPrefix + customeriD.ToString() + "-" +
                                                 maxAmount + "-" + DateTime.Now.ToString("yy");

            return transactionNumber;
        }


        public override void Add(StockTransaction stockTransaction)
        {
            var maxAmount = 
            (this.Count(W => W.customer.ID == stockTransaction.customer.ID && W.Date.Year == DateTime.Now.Year && !W.IsDeleted) + 1).ToString();

            stockTransaction.TransactionNumber = TranNumberPrefix + stockTransaction.customer.ID.ToString() + "-" +
                                                 maxAmount + "-" + DateTime.Now.ToString("yy");

            base.Add(stockTransaction);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public decimal CalculateBalanceForStockItem(StockItem item, DateTime date)
        {
            return GetFilteredCollection(W => W.stockItem.ID == item.ID && W.Date.Date == date.Date && !W.IsDeleted).Select(S => S.Amount).ToList().Sum(); ;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public decimal CalculateBalanceForStockItemAfterDate(StockItem item, DateTime date)
        {
            return GetFilteredCollection(W => W.stockItem.ID == item.ID && W.Date.Date >= date.Date && !W.IsDeleted).Select(S => S.Amount).ToList().Sum();
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public decimal CalculateBalanceForStockItemBeforeDate(StockItem item, DateTime date)
        {
            return GetFilteredCollection(W => W.stockItem.ID == item.ID && W.Date.Date <= date.Date && !W.IsDeleted).Select(S => S.Amount).ToList().Sum();
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="stockItemList"></param>
        /// <param name="customerList"></param>
        /// <param name="fillDirection"></param>
        public void FillData(StockItemList stockItemList, CustomerList customerList, DateTime date, FillDirection fillDirection)
        {
            var idCounter = 1;

            const int step = 10;

            for (var j = 0; j < step; j++)
            {
                foreach (var oneCustomer in customerList)
                {

                    foreach (var onestockItem in stockItemList)
                    {
                        Add(new StockTransaction(Session) { ID = idCounter++, Amount = j*10 + idCounter + onestockItem.ID, stockItem = onestockItem, customer = oneCustomer,

                            Date =
                            fillDirection == FillDirection.Beforedate ?
                               date.Date.AddHours( -(idCounter + oneCustomer.ID + onestockItem.ID) )
                               :
                               date.Date.AddHours(idCounter + oneCustomer.ID + onestockItem.ID)

                            , TransactionNumber = GetTransactionNumber(oneCustomer.ID) });
                    }
                }

            }
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="sesssion"></param>
        /// <param name="stockItemList"></param>
        /// <param name="customerList"></param>
        /// <returns></returns>
        public static StockTransactionList CreateNew(Session sesssion, StockItemList stockItemList, CustomerList customerList, DateTime date, FillDirection fillDirection)
        {
            var tmpStockTransaction = new StockTransactionList(sesssion);

            tmpStockTransaction.FillData(stockItemList, customerList, date, fillDirection);

            return tmpStockTransaction;
        }

        

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public StockBalanceList GetBalanceList()
        {
            var tmpStockBalanceList = new StockBalanceList(Session);

            var tmpStockItemsList = this.Select(S => new { stockItemID = S.stockItem.ID, customerID = S.customer.ID }).Distinct().ToList().AsEnumerable();

            foreach (var tmpStockItemid in tmpStockItemsList)
            {
                var oneStockItemList = this.Where(W => (W.stockItem.ID == tmpStockItemid.stockItemID) && (W.customer.ID == tmpStockItemid.customerID)).ToList();

                if  (oneStockItemList.Any() )
                    tmpStockBalanceList.AddNewBalance(oneStockItemList.FirstOrDefault().customer,
                        oneStockItemList.FirstOrDefault().stockItem,
                        oneStockItemList.Select(s => s.Amount).Sum());
                ;
            }


            return tmpStockBalanceList;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerid"></param>
        /// <returns></returns>
        public StockBalanceList GetBalanceListByCustomer( int customerid)
        {
            var tmpStockBalanceList = new StockBalanceList(Session);

            var tmpStockItemsList = this.Where(w => w.customer.ID == customerid).Select(S => new { stockItemID = S.stockItem.ID, customerID = S.customer.ID }).Distinct().ToList().AsEnumerable();

            foreach (var tmpStockItemid in tmpStockItemsList)
            {
                var oneStockItemList = this.Where(W => (W.stockItem.ID == tmpStockItemid.stockItemID) && (W.customer.ID == tmpStockItemid.customerID)).ToList();

                if (oneStockItemList.Any())
                    tmpStockBalanceList.AddNewBalance(oneStockItemList.FirstOrDefault().customer,
                        oneStockItemList.FirstOrDefault().stockItem,
                        oneStockItemList.Select(s => s.Amount).Sum());
                ;
            }


            return tmpStockBalanceList;
        }


        /// <summary>
        /// /
        /// </summary>
        /// <param name="stockItemid"></param>
        /// <returns></returns>
        public StockBalanceList GetBalanceListByStockItem(int stockItemid)
        {
            var tmpStockBalanceList = new StockBalanceList(Session);

            var tmpStockItemsList = this.Where(w => w.stockItem.ID == stockItemid).Select(S => new { stockItemID = S.stockItem.ID, customerID = S.customer.ID }).Distinct().ToList().AsEnumerable();

            foreach (var tmpStockItemid in tmpStockItemsList)
            {
                var oneStockItemList = this.Where(W => (W.stockItem.ID == tmpStockItemid.stockItemID) && (W.customer.ID == tmpStockItemid.customerID)).ToList();

                if (oneStockItemList.Any())
                    tmpStockBalanceList.AddNewBalance(oneStockItemList.FirstOrDefault().customer,
                        oneStockItemList.FirstOrDefault().stockItem,
                        oneStockItemList.Select(s => s.Amount).Sum());
                ;
            }


            return tmpStockBalanceList;
        }



        /// <summary>
        /// /
        /// </summary>
        /// <param name="stockItemid"></param>
        /// <param name="customerid"></param>
        /// <returns></returns>
        public StockBalanceList GetBalanceListByStockItemCustomer(int stockItemid, int customerid)
        {
            var tmpStockBalanceList = new StockBalanceList(Session);

            var tmpStockItemsList = this.Where(w => w.stockItem.ID == stockItemid && w.customer.ID == customerid).Select(S => new { stockItemID = S.stockItem.ID, customerID = S.customer.ID }).Distinct().ToList().AsEnumerable();

            foreach (var tmpStockItemid in tmpStockItemsList)
            {
                var oneStockItemList = this.Where(W => (W.stockItem.ID == tmpStockItemid.stockItemID) && (W.customer.ID == tmpStockItemid.customerID)).ToList();

                if (oneStockItemList.Any())
                    tmpStockBalanceList.AddNewBalance(oneStockItemList.FirstOrDefault().customer,
                        oneStockItemList.FirstOrDefault().stockItem,
                        oneStockItemList.Select(s => s.Amount).Sum());
                ;
            }


            return tmpStockBalanceList;
        }
    }


}
