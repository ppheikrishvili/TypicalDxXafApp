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
        public string Name
        {
            get { return GetPropertyValue(() => Name); }
        
            set { SetPropertyValue(() => Name, value); }
        }
    }
}
