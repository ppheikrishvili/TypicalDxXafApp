using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Xpo;

namespace TypicalDXeXpressAppProject_DoSo.Module._Specs.ClassLibrary
{
    public interface IBaseClass
    {
    }



    public partial class BaseClass : XPObject
    {

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



}
