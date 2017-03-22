using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;


using DevExpress.ExpressApp.Filtering;
using Shouldly;
using TypicalDXeXpressAppProject_DoSo.Module.BusinessObjects;

namespace TypicalDXeXpressAppProject_DoSo.Module._Specs.ClassLibrary
{
    public interface IBaseClass
    {
    }


    public partial class BaseClass : XPLiteObjectBase
    {

        //public BaseClass(){}


        public BaseClass(Session session) : base(session) { }



        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="someProperty"></param>
        /// <param name="_value"></param>
        public virtual void SetPropertyValue<TProperty>(Expression<Func<TProperty>> someProperty, TProperty _value)
        {
            MemberExpression tmpMemberExp = someProperty.Body as MemberExpression;

            SetPropertyValue(tmpMemberExp.Member.Name, _value);
        }



        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="someProperty"></param>
        /// <returns></returns>
        public virtual TProperty GetPropertyValue<TProperty>(Expression<Func<TProperty>> someProperty)
        {
            MemberExpression tmpMemberExp = someProperty.Body as MemberExpression;

            return GetPropertyValue<TProperty>(tmpMemberExp.Member.Name);
        }

    }








    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseClassList<T> : XPCollection<T>, IBaseClass where T : XPLiteObjectBase
    {

        public BaseClassList(Session sesssion) : base(sesssion)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        //protected BaseClassList()
        //{
        //}

        public T GetItemByid(int id)
        {
            return this.FirstOrDefault(w => w.ID == id && !w.IsDeleted);
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public T GetItemBypos(int pos)
        {
            this.Count.ShouldBeLessThan(pos);
            return this[pos];
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="whereFunc"></param>
        /// <returns></returns>
        public XPCollection<T> GetFilteredCollection(Func<T, bool> whereFunc)
        {
            return new XPCollection<T>(Session, this.Where(whereFunc).ToList());
        }


        public new virtual void Add(T initilaOneData)
        {
            base.Add(initilaOneData);
        }


        /// <summary>
        /// 
        /// </summary>
        public BaseClassList(IEnumerable<T> initilaData)
        {
            this.AddRange(initilaData);
        }

    }







}
