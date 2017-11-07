using System;
using System.Linq.Expressions;

namespace GenericSearch.Expressions.ExpressionStrategyHandlers
{
    public class ExpressionHandlerByRange : IPrepareExpressionBaseOnStrategy
    {
        public SearchClauseStrategy SearchStrategy { get; set; }
        
        public Expression<Func<T, bool>> CreateExpression<T>(ISearchableEntity entity, ParameterExpression expressionParameter, MemberExpression memberExpression)
        {
            if (entity.AdditionalValue == null || entity.ValueToSearch == null)
            {
                throw new ArgumentException("Search by range requires not empty values");
            }

            if (entity.ValueToSearch.GetType() != entity.AdditionalValue.GetType())
            {
                throw new ArgumentException("Search by range with parameters of different type is not supported");
            }
    
            var firstExpression = Expression.GreaterThanOrEqual(
                memberExpression,
                Expression.Constant(entity.ValueToSearch, entity.ValueType));

            var secondExpression = Expression.LessThanOrEqual(
                memberExpression,
                Expression.Constant(entity.AdditionalValue, entity.ValueType));

            return Expression.Lambda<Func<T, bool>>(
                Expression.AndAlso(firstExpression, secondExpression),
                expressionParameter);
        }

        public ExpressionHandlerByRange()
        {
            SearchStrategy = SearchClauseStrategy.ByRange;
        }
    }
}