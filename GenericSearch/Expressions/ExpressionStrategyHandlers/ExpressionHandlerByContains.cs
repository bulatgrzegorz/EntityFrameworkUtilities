using System;
using System.Linq.Expressions;

namespace GenericSearch.Expressions.ExpressionStrategyHandlers
{
    public class ExpressionHandlerByContains : IPrepareExpressionBaseOnStrategy
    {
        public SearchClauseStrategy SearchStrategy { get; set; }
        
        public Expression<Func<T, bool>> CreateExpression<T>(ISearchableEntity entity)
        {
            if (entity.ValueType != typeof(string))
            {
                //Stupid rider cant understand string interpolation
                throw new ArgumentException(string.Format("Search using contains strategy is supported only for {0} type, not for type {1}.",new object[]{ typeof(string).Name, entity.ValueType.Name}));
            }

            var expressionParameter = Expression.Parameter(type: typeof(T));
            
            var containsMethodInfo = typeof(string).GetMethod(name: "Contains", types: new[] {typeof(string)});
            var expressionMember = Expression.Property(expression: expressionParameter, propertyName: entity.ColumnNameToSearchBy);
            var constant = Expression.Constant(value: entity.ValueToSearch, type: entity.ValueType);
            var expressionMethodCall = Expression.Call(expressionMember, containsMethodInfo, constant);

            return Expression.Lambda<Func<T, bool>>(
                expressionMethodCall, expressionParameter);
        }

        public ExpressionHandlerByContains()
        {
            SearchStrategy = SearchClauseStrategy.ByContains;
        }
    }
}