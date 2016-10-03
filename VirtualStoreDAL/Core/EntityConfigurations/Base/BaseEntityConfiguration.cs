using Pisa.VirtualStore.Models.Base;
using System;
using System.Data.Entity.ModelConfiguration;
using System.Linq.Expressions;

namespace Pisa.VirtualStore.Dal.Core.EntityConfigurations.Base
{
    class BaseEntityConfiguration<M> : EntityTypeConfiguration<M> where M : class, IBaseModel
    {
        public void MakeRequired<TargetEntity>(Expression<Func<M, TargetEntity>> navigationPropertyExpression) where TargetEntity : class
        {
            // This function calls the Required method and then the Map like this:
            // this.HasRequired<SecurityPerson>(fk => fk.SecurityPerson).WithOptional().Map(m => m.MapKey("IdSecurityPerson"));
            //string propertyName = ((MemberExpression)navigationPropertyExpression.Body).Member.Name;
            //this.HasRequired<TargetEntity>(navigationPropertyExpression).WithOptional().Map(m => m.MapKey("Id" + propertyName));
        }

        /// <summary>
        /// Mark the navigation property as optional. 
        /// NOTE: make sure the foreign Key Property is of the type int?
        /// </summary>
        /// <typeparam name="TargetEntity"></typeparam>
        /// <param name="navigationPropertyExpression"></param>
        /// <param name="foreignKeyPropertyExpression"></param>
        public void MakeOptional<TargetEntity>(Expression<Func<M, TargetEntity>> navigationPropertyExpression, Expression<Func<M, int?>> foreignKeyPropertyExpression) where TargetEntity : class
        {
            // This function calls the HasOptional method and then the Map like this:
            // this.HasOptional<SecurityAccount>(fk => fk.LastAccountUsed).WithOptionalDependent().Map(m => m.MapKey("LastAccountUsedId"));
            //string propertyName = ((MemberExpression)navigationPropertyExpression.Body).Member.Name;
            //this.HasOptional<TargetEntity>(navigationPropertyExpression).WithRequired().Map(m => m.MapKey(propertyName + "Id"));
            this.HasOptional<TargetEntity>(navigationPropertyExpression).WithMany().HasForeignKey<int?>(foreignKeyPropertyExpression);
        }
/*
        public void MakeSelfRefereceOptional<TargetEntity>(Expression<Func<M, TargetEntity>> navigationPropertyExpression, Expression<Func<M, int?>> foreignKeyPropertyExpression) where TargetEntity : class
        {
            // This function calls the HasOptional method and then the Map like this:
            // this.HasOptional(fk => fk.StoreParent).WithMany(fk => fk.Children).HasForeignKey(fk => fk.StoreParentId).WillCascadeOnDelete(false);
            this.HasOptional<TargetEntity>(navigationPropertyExpression).WithMany().HasForeignKey<int?>(foreignKeyPropertyExpression);
        }
*/
    }
}
