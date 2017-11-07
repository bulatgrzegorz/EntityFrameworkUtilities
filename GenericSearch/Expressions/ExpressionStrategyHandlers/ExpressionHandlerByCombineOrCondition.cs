using System;
using System.Collections;
using System.Linq.Expressions;

namespace GenericSearch.Expressions.ExpressionStrategyHandlers
{
    public class ExpressionHandlerByCombineOrCondition : IPrepareExpressionBaseOnStrategy
    {
        public SearchClauseStrategy SearchStrategy { get; set; }

        public Expression<Func<T, bool>> CreateExpression<T>(
            ISearchableEntity entity, 
            ParameterExpression parameterExpression,
            MemberExpression memberExpression)
        {
            if (!(entity.ValueToSearch is IList))
            {
                throw new ArgumentException("Type of value to search by is wrong.");
            }

            var valueConvertedToList = (IList) entity.ValueToSearch;

            if (valueConvertedToList.Count == 0) return null;
            if (valueConvertedToList.Count == 1)
            {
                var normalExpressionHandler = new ExpressionHandlerNormal();
                return normalExpressionHandler.CreateExpression<T>(entity, parameterExpression, memberExpression);
            }

            return null;
        }

        public ExpressionHandlerByCombineOrCondition()
        {
            SearchStrategy = SearchClauseStrategy.CombiningByOr;
        }
    }
}