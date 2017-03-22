using System;
using System.Collections.Generic;
using Shouldly;
using TypicalDXeXpressAppProject_DoSo.Module.BusinessObjects;
using TypicalDXeXpressAppProject_DoSo.Module._Specs.ClassLibrary;
using Xunit;

namespace TypicalDXeXpressAppProject_DoSo.Module._Specs
{
    public class StockTests : XpoTestsBase
    {
        /// <summary>
        /// Create StockTransactions in this test for the StockItem BEFORE the date you are trying to calculate BalanceAmount for.
        /// Simply write a method somewhere that accepts DateTime and StockItem and returns the amount of StockItems available for that Date.
        /// </summary>
        [Fact]
        public void When_calculating_StockItemBalance_for_date_BalanceAmount_should_be_sum_of_all_previous_transactions_amounts()
        {
            //StockTransactionMethods.CalculateBalanceForStockItem(StockItem item, DateTime date)..

            // Session.CreateObjectTypeRecords


            var tmpCustomerList = CustomerList.CreateNew(Uow);

            var tmpStockItemist = StockItemList.CreateNew(Uow);

            var tmpStockTransactionList = StockTransactionList.CreateNew(Uow, tmpStockItemist, tmpCustomerList, new DateTime(2017, 03, 03), StockTransactionList.FillDirection.Beforedate);

            var count = tmpStockTransactionList.CalculateBalanceForStockItem(tmpStockItemist.GetItemByid(9), new DateTime(2017, 02, 27));

            //var customer = new BaseClass(Uow);
            //Uow.CommitChanges();
        }



        /// <summary>
        /// Create StockTransactions in this test for the StockItem AFTER the date you are trying to calculate BalanceAmount for.
        /// Simply write a method somewhere that accepts DateTime and StockItem and returns the amount of StockItems available for that Date.
        /// </summary>
        [Fact]
        public void When_calculating_StockItemBalance_for_date_BalanceAmount_should_not_consider_future_transaction_amounts()
        {
            //var tmpStockTransactionList = new StockTransactionList(Uow);

            //tmpStockTransactionList.Add(new StockTransaction(Uow) { stockItem = new StockItem(Uow) { ID = 1, Name = "Item1" }, Amount = 12, Date = new DateTime(2017, 01, 01), ID = 1, customer = new Customer(Uow) });
            //tmpStockTransactionList.Add(new StockTransaction(Uow) { stockItem = new StockItem(Uow) { ID = 1, Name = "Item1" }, Amount = 1, Date = new DateTime(2017, 01, 02), ID = 2, customer = new Customer(Uow) });
            //tmpStockTransactionList.Add(new StockTransaction(Uow) { stockItem = new StockItem(Uow) { ID = 3, Name = "Item3" }, Amount = 14, Date = new DateTime(2017, 01, 03), ID = 3, customer = new Customer(Uow) });

            var tmpCustomerList = CustomerList.CreateNew(Uow);

            var tmpStockItemist = StockItemList.CreateNew(Uow);

            var tmpStockTransactionList = StockTransactionList.CreateNew(Uow, tmpStockItemist, tmpCustomerList, new DateTime(2017, 01, 03), StockTransactionList.FillDirection.Afterdate);

            var retResult = tmpStockTransactionList.CalculateBalanceForStockItem(tmpStockItemist.GetItemByid(1), new DateTime(2017, 01, 05));
        }
        /// <summary>
        /// For example, if there are two customers with IDs 105 & 203, StockTransactions created should be numbered as ST-105-1-17, ST-105-2-17 and ST-203-1-17, ST-203-2-17
        /// where last part is the StockTransaction's Year, second to last is the incrementing transaction identity for that customer for the year 2017
        /// </summary>
        [Fact]
        public void StockTransaction_Number_should_be_calculated_for_the_Customer_and_Year_of_the_StockTransaction()
        {
            //var tmpStockTransactionList = new StockTransactionList(Uow);

            //tmpStockTransactionList.Add(new StockTransaction(Uow) { stockItem = new StockItem(Uow) { ID = 1, Name = "Item1" }, Amount = 12, Date = new DateTime(2017, 01, 01), ID = 1, customer = new Customer(Uow) });
            //tmpStockTransactionList.Add(new StockTransaction(Uow) { stockItem = new StockItem(Uow) { ID = 1, Name = "Item1" }, Amount = 1, Date = new DateTime(2017, 01, 02), ID = 2, customer = new Customer(Uow) });
            //tmpStockTransactionList.Add(new StockTransaction(Uow) { stockItem = new StockItem(Uow) { ID = 3, Name = "Item3" }, Amount = 14, Date = new DateTime(2017, 01, 03), ID = 3, customer = new Customer(Uow) });


            var tmpCustomerList = CustomerList.CreateNew(Uow);

            var tmpStockItemist = StockItemList.CreateNew(Uow);

            var tmpStockTransactionList = StockTransactionList.CreateNew(Uow, tmpStockItemist, tmpCustomerList, DateTime.Now.Date, StockTransactionList.FillDirection.Beforedate);

            tmpStockTransactionList.CalculateBalanceForStockItem(tmpStockItemist.GetItemByid(1), new DateTime(2017, 3, 20));
        }

        /// <summary>
        /// Customer should have a collection of StockItemBalances which should be updated or created based on the StockTransactions taking place.
        /// Thus, if you create a new StockTransaction, you should see if StockItemBalance exists for this StockItem for this Customer, create if it doesn't and update if it does.
        /// </summary>
        [Fact]
        public void StockItemBalance_should_be_ADDED_to_customer_if_it_doesnt_exist()
        {
            var tmpStockBalanceList = new StockBalanceList(Uow);
            var customer = new Customer(Uow);

            tmpStockBalanceList.AddNewBalance(customer, new StockItem(Uow) { ID = 1, Name = "Item1" }, 100);
            tmpStockBalanceList.AddNewBalance(customer, new StockItem(Uow) { ID = 2, Name = "Item2" }, 10);
            tmpStockBalanceList.AddNewBalance(customer, new StockItem(Uow) { ID = 3, Name = "Item13" }, 120);
            tmpStockBalanceList.AddNewBalance(customer, new StockItem(Uow) { ID = 4, Name = "Item4" }, 1);

            tmpStockBalanceList.Count.ShouldBe(4);
        }

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void StockItemBalance_should_be_UPDATED_if_it_exists_for_customer()
        {
            var tmpStockBalanceList = new StockBalanceList(Uow);

            var customer = new Customer(Uow);

            var stockItem1 = new StockItem(Uow) { ID = 1, Name = "Item1" };
            var stockItem2 = new StockItem(Uow) { ID = 2, Name = "Item2" };

            tmpStockBalanceList.AddNewBalance(customer, stockItem1, 100);
            tmpStockBalanceList.AddNewBalance(customer, stockItem1, 10);
            tmpStockBalanceList.AddNewBalance(customer, stockItem2, 120);
            tmpStockBalanceList.AddNewBalance(customer, stockItem2, 1);

            tmpStockBalanceList.Count.ShouldBe(2);
        }
    }
}