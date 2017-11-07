using System;
using System.Linq.Expressions;

namespace GenericSearch.Expressions.ExpressionStrategyHandlers
{
    public interface IPrepareExpressionBaseOnStrategy
    {
        SearchClauseStrategy SearchStrategy { get; set; }

        Expression<Func<T, bool>> CreateExpression<T>(ISearchableEntity entity, ParameterExpression parameterExpression);
    }
}