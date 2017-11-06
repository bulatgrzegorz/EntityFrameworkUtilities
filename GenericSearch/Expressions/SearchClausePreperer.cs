using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using GenericSearch.Expressions.ExpressionStrategyHandlers;

namespace GenericSearch.Expressions
{
    public class SearchClausePreperer : ICreateSearchClause
    {
        private readonly ICollection<IPrepareExpressionBaseOnStrategy> _expressionPreparersBaseOnStrategy;

        public SearchClausePreperer(
            IEnumerable<IPrepareExpressionBaseOnStrategy> expressionPreparersBaseOnStrategy)
        {
            _expressionPreparersBaseOnStrategy = expressionPreparersBaseOnStrategy.ToList();
        }

        public Expression<Func<T, bool>> CreateSearchClause<T>(ISearchableEntity entity)
        {
            var expressionParameter = Expression.Parameter(typeof(T));
            
            return Expression.Lambda<Func<T, bool>>(
                Expression.Equal(
                    left: Expression.Property(expression: expressionParameter, propertyName: entity.ColumnNameToSearchBy),
                    right: Expression.Constant(value: entity.ValueToSearch, type: entity.ValueType)),
                expressionParameter);
        }
    }
}