using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;


using DevExpress.ExpressApp.Filtering;

namespace TypicalDXeXpressAppProject_DoSo.Module._Specs.ClassLibrary
{
    public interface IBaseClass
    {
    }


    public partial class BaseClass : XPLiteObject
    {

        public BaseClass()
        {
        }


        public BaseClass(Session session) : base(session) { }



        [Key(true)]
        [VisibleInListView(true)]
        public int ID
        {
            get { return GetPropertyValue(() => ID); }
            set { SetPropertyValue("ID", value); }
        }

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
    public abstract class BaseClassList<T> : XPCollection<T>, IBaseClass where T : BaseClass
    {

        public BaseClassList(Session sesssion) : base(sesssion)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public BaseClassList()
        {
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
