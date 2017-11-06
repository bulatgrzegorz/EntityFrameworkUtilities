using System;
using System.Linq.Expressions;

namespace GenericSearch.Expressions.ExpressionStrategyHandlers
{
    public class ExpressionHandlerNormal : IPrepareExpressionBaseOnStrategy
    {
        public SearchClauseStrategy SearchStrategy { get; set; }

        public Expression<Func<T, bool>> CreateExpression<T>(ISearchableEntity entity)
        {
            var expressionParameter = Expression.Parameter(typeof(T));
            
            return Expression.Lambda<Func<T, bool>>(
                Expression.Equal(
                    left: Expression.Property(expression: expressionParameter, propertyName: entity.ColumnNameToSearchBy),
                    right: Expression.Constant(value: entity.ValueToSearch, type: entity.ValueType)),
                expressionParameter);
        }

        public ExpressionHandlerNormal()
        {      
            //another time when rider is stupid as fuck, no matter that it allows you to use C#6, default property value is to hard to understand...
            SearchStrategy = SearchClauseStrategy.Normal;
        }
    }
}