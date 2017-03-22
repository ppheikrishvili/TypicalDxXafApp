using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Xpo;
using Shouldly;

namespace TypicalDXeXpressAppProject_DoSo.Module._Specs.ClassLibrary
{
    public class StockItem : BaseClass, IBaseClass
    {
        public StockItem(Session session) : base(session)
        {
        }

        public string Name
        {
            get { return GetPropertyValue(() => Name); }
            set { SetPropertyValue(() => Name, value); }
        }
    }



    /// <summary>
    /// 
    /// </summary>
    public partial class StockItemList : BaseClassList<StockItem>
    {


        public StockItemList(Session sesssion) : base(sesssion)
        {

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="stockItem"></param>
        public override void Add(StockItem stockItem)
        {
            base.Add(stockItem);
        }


        /// <summary>
        /// 
        /// </summary>
        public void FillData()
        {
            for (var i = 1; i < 10; i++)
            {
                this.Add(new StockItem(Session) {ID = i, Name = "StockItem - " + i.ToString()});
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sesssion"></param>
        /// <returns></returns>
        public static StockItemList CreateNew(Session sesssion)
        {
            var tmpStockItemList = new StockItemList(sesssion);

            tmpStockItemList.FillData();

            return tmpStockItemList;

        }
    }


}
