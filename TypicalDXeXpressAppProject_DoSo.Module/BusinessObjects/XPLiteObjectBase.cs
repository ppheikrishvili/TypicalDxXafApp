using DevExpress.Xpo;

namespace TypicalDXeXpressAppProject_DoSo.Module.BusinessObjects
{
    [NonPersistent]
    // ReSharper disable once InconsistentNaming
    public class XPLiteObjectBase : XPLiteObject
    {
        public XPLiteObjectBase(Session session) : base(session) { }

        [Key(true)]
        // ReSharper disable once InconsistentNaming
        public int ID { get; set; }
    }
}