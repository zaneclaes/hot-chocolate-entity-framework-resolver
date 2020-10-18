using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;

namespace OpenWorkShop.Data.GraphqlExtensions {
  public static class EntityResloverExtensions {
    private static string GetName<TSourceType, TProperty>(this Expression<Func<TSourceType, TProperty>> expression) {
      try {
        MemberExpression? member = (MemberExpression) expression.Body;
        return member.Member.Name;
      } catch {
        throw new ArgumentException($"Could not determine name of member expression from {expression}");
      }
    }

    public static IObjectFieldDescriptor Entity<TKey, TData, TProp, TDb>(
      this IObjectTypeDescriptor<TData> desc, Expression<Func<TData, ICollection<TProp>>> propertyOrMethod
    ) where TData : IModelId<TKey> where TProp : class, IModelId<TKey> where TKey : class where TDb : DbContext =>
      desc.Field(propertyOrMethod.GetName())
          .ResolveWith<EntityFrameworkResolver<TKey, TData, TProp, TDb>>(r =>
             r.ManyToMany(default!, default!, default!));
  }
}
