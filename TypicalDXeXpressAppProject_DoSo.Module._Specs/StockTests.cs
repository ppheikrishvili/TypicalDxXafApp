using System;
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

             var tmpStockTransactionList = new StockTransactionList();

            tmpStockTransactionList.AddOne(new StockTransaction() { stockItem = new StockItem() { ID = 1, Name = "Item1" }, Amount = 12, Date = new DateTime(2017, 01, 01), ID = 1, customer = new Customer(Uow) });

            tmpStockTransactionList.AddOne(new StockTransaction() { stockItem = new StockItem() { ID = 1, Name = "Item1" }, Amount = 1, Date = new DateTime(2017, 01, 02), ID = 2, customer = new Customer(Uow) });

            tmpStockTransactionList.AddOne(new StockTransaction() { stockItem = new StockItem() { ID = 3, Name = "Item3" }, Amount = 14, Date = new DateTime(2017, 01, 03), ID = 3, customer = new Customer(Uow) });

            tmpStockTransactionList.CalculateBalanceForStockItem(new StockItem() { ID = 1, Name = "Item1" },
                new DateTime(2017, 01, 03));

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
            var tmpStockTransactionList = new StockTransactionList();

            tmpStockTransactionList.AddOne(new StockTransaction() { stockItem = new StockItem() { ID = 1, Name = "Item1" }, Amount = 12, Date = new DateTime(2017, 01, 01), ID = 1, customer = new Customer(Uow) });

            tmpStockTransactionList.AddOne(new StockTransaction() { stockItem = new StockItem() { ID = 1, Name = "Item1" }, Amount = 1, Date = new DateTime(2017, 01, 02), ID = 2, customer = new Customer(Uow) });

            tmpStockTransactionList.AddOne(new StockTransaction() { stockItem = new StockItem() { ID = 3, Name = "Item3" }, Amount = 14, Date = new DateTime(2017, 01, 03), ID = 3, customer = new Customer(Uow) });

            tmpStockTransactionList.CalculateBalanceForStockItemAfterDate(new StockItem() { ID = 1, Name = "Item1" },
                new DateTime(2017, 01, 03));
        }
        /// <summary>
        /// For example, if there are two customers with IDs 105 & 203, StockTransactions created should be numbered as ST-105-1-17, ST-105-2-17 and ST-203-1-17, ST-203-2-17
        /// where last part is the StockTransaction's Year, second to last is the incrementing transaction identity for that customer for the year 2017
        /// </summary>
        [Fact]
        public void StockTransaction_Number_should_be_calculated_for_the_Customer_and_Year_of_the_StockTransaction()
        {
            var tmpStockTransactionList = new StockTransactionList();

            tmpStockTransactionList.AddOne(new StockTransaction() { stockItem = new StockItem() { ID = 1, Name = "Item1" }, Amount = 12, Date = new DateTime(2017, 01, 01), ID = 1, customer = new Customer(Uow) });

            tmpStockTransactionList.AddOne(new StockTransaction() { stockItem = new StockItem() { ID = 1, Name = "Item1" }, Amount = 1, Date = new DateTime(2017, 01, 02), ID = 2, customer = new Customer(Uow) });

            tmpStockTransactionList.AddOne(new StockTransaction() { stockItem = new StockItem() { ID = 3, Name = "Item3" }, Amount = 14, Date = new DateTime(2017, 01, 03), ID = 3, customer = new Customer(Uow) });
        }

        /// <summary>
        /// Customer should have a collection of StockItemBalances which should be updated or created based on the StockTransactions taking place.
        /// Thus, if you create a new StockTransaction, you should see if StockItemBalance exists for this StockItem for this Customer, create if it doesn't and update if it does.
        /// </summary>
        [Fact]
        public void StockItemBalance_should_be_ADDED_to_customer_if_it_doesnt_exist()
        {
            var tmpStockBalanceList = new StockBalanceList();

            tmpStockBalanceList.AddNewBalance(new Customer(Uow), new StockItem() {ID = 1, Name = "Item1"}, 100);

            tmpStockBalanceList.AddNewBalance(new Customer(Uow), new StockItem() { ID = 2, Name = "Item2" }, 10);

            tmpStockBalanceList.AddNewBalance(new Customer(Uow), new StockItem() { ID = 3, Name = "Item13" }, 120);

            tmpStockBalanceList.AddNewBalance(new Customer(Uow), new StockItem() { ID = 4, Name = "Item4" }, 1);
        }

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void StockItemBalance_should_be_UPDATED_if_it_exists_for_customer()
        {
            var tmpStockBalanceList = new StockBalanceList();

            tmpStockBalanceList.AddNewBalance(new Customer(Uow), new StockItem() { ID = 1, Name = "Item1" }, 100);

            tmpStockBalanceList.AddNewBalance(new Customer(Uow), new StockItem() { ID = 2, Name = "Item2" }, 10);

            tmpStockBalanceList.AddNewBalance(new Customer(Uow), new StockItem() { ID = 2, Name = "Item2" }, 120);

            tmpStockBalanceList.AddNewBalance(new Customer(Uow), new StockItem() { ID = 1, Name = "Item1" }, 1);
        }

    }
}