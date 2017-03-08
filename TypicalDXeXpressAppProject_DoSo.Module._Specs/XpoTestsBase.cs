using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using Shouldly;

namespace TypicalDXeXpressAppProject_DoSo.Module._Specs
{
    public class XpoTestsBase
    {
        UnitOfWork _uow;
        protected UnitOfWork Uow => _uow ?? (_uow = GetUow());
        protected static UnitOfWork GetUow() => new UnitOfWork();
        protected XpoTestsBase()
        {
            XpoDefault.DataLayer = new SimpleDataLayer(new InMemoryDataStore());
        }

        protected IList<T> Get<T>(Expression<Func<T, bool>> what2Get = null) where T : XPBaseObject
        {
            what2Get = what2Get ?? (f => true);

            var objects2Return = new List<T>();
            foreach (var tObject in Uow.Query<T>().Where(what2Get))
            {
                tObject.Reload();
                objects2Return.Add(tObject);
            }

            return objects2Return;
        }

        protected T GetOne<T>(Expression<Func<T, bool>> what2Get = null) where T : XPBaseObject
        {
            Get(what2Get).Count.ShouldBe(1);
            return Get(what2Get).Single();
        }

        protected void ShouldBeOne<T>(Expression<Func<T, bool>> what2Get = null) where T : XPBaseObject => GetOne(what2Get);

        protected T GetReloaded<T>(T object2Reload) where T : XPBaseObject
        {
            Uow.Reload(object2Reload);
            return object2Reload;
        }
    }
}
