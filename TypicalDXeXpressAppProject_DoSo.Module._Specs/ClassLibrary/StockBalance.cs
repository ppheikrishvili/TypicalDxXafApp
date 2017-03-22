using System;
using System.Linq;
using DevExpress.Xpo;
using TypicalDXeXpressAppProject_DoSo.Module.BusinessObjects;

namespace TypicalDXeXpressAppProject_DoSo.Module._Specs.ClassLibrary
{
    public class StockBalance : BaseClass, IBaseClass
    {
        //public StockBalance() : base() { }

        public StockBalance(Session session) : base(session) { }


        public Customer customer
        {
            get { return GetPropertyValue(() => customer); }
            set { SetPropertyValue(() => customer, value); }
        }

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


        /// <summary>
        /// 
        /// </summary>
        protected override void OnSaving()
        {
            base.OnSaving();


            if (Amount < 0)
            {
                throw new Exception("Negative amount!");
            }

        }
    }






    /// <summary>
    /// 
    /// </summary>
    public partial class StockBalanceList : BaseClassList<StockBalance>
    {
        public StockBalanceList(Session sesssion) : base(sesssion){}


        //public StockBalanceList() : base()
        //{

        //}


        /// <summary>
        /// 
        /// </summary>
        /// <param name="stockBalance"></param>
        public override void Add(StockBalance stockBalance)
        {
            var tmpStockBalance =
                this.SingleOrDefault(W => ((W.customer.ID == stockBalance.customer.ID) && ( W.stockItem.ID == stockBalance.stockItem.ID) && !W.IsDeleted));

            
            if (tmpStockBalance != null)
                tmpStockBalance.Amount += stockBalance.Amount; //igulisxmeba rom amatebs
            else
                base.Add(stockBalance);
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="stockItem"></param>
        /// <param name="amount"></param>
        public void AddNewBalance(Customer customer,  StockItem stockItem, decimal amount)
        {
            var tmpStockBalance =
                this.SingleOrDefault(W => ((W.customer.ID == customer.ID) && (W.stockItem.ID == stockItem.ID) && !W.IsDeleted));

            if (tmpStockBalance != null)
                tmpStockBalance.Amount += amount; //igulisxmeba rom amatebs

            else
                this.Add(new StockBalance(Session)
                {
                    ID = (
                    this.Count > 0 ?
                    this.Max(m => m.ID) + 1 : 1),
                    stockItem = stockItem,
                    Amount = amount,
                    customer = customer
                });
        }
    }
}
