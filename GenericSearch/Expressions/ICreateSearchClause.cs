using System;
using System.Linq.Expressions;

namespace GenericSearch.Expressions
{
    public interface ICreateSearchClause
    {
        Expression<Func<T, bool>> CreateSearchClause<T>(ISearchableEntity entity);
    }
}