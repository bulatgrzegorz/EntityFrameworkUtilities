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
            var searchHandler =
                _expressionPreparersBaseOnStrategy.SingleOrDefault(x => x.SearchStrategy == entity.SearchStrategy);
            if (searchHandler == null)
            {
                throw new InvalidOperationException(
                    string.Format("Search by {0} strategy is not actualy supported", entity.SearchStrategy));
            }

            return searchHandler.CreateExpression<T>(entity, Expression.Parameter(typeof(T)));

        }
    }
}