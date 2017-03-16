using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Xpo;

namespace TypicalDXeXpressAppProject_DoSo.Module._Specs.ClassLibrary
{
    public partial class StockItem : BaseClass, IBaseClass
    {
        public int Id
        {
            get { return GetPropertyValue( () => Id); }
            set { SetPropertyValue(() => Id, value); }
        }
    }
}
