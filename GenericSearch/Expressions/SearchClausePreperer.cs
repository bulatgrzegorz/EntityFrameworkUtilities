using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using GenericSearch.Expressions.ExpressionStrategyHandlers;
using GenericSearch.Helpers;

namespace GenericSearch.Expressions
{
    public class SearchClausePreperer : ICreateSearchClause
    {
        private readonly ICollection<IPrepareExpressionBaseOnStrategy> _expressionPreparersBaseOnStrategy;
        private readonly IConvertTypeToPrecise _convertTypeToPrecise;

        public SearchClausePreperer(
            IEnumerable<IPrepareExpressionBaseOnStrategy> expressionPreparersBaseOnStrategy, 
            IConvertTypeToPrecise convertTypeToPrecise)
        {
            _expressionPreparersBaseOnStrategy = expressionPreparersBaseOnStrategy.ToList();
            _convertTypeToPrecise = convertTypeToPrecise;
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

            entity = ConvertEntityValuesToPreciseTypes(entity);

            var expressionParameter = Expression.Parameter(typeof(T));

            return searchHandler.CreateExpression<T>(entity, expressionParameter, Expression.Property(expressionParameter, entity.ColumnNameToSearchBy));
        }

        private ISearchableEntity ConvertEntityValuesToPreciseTypes(ISearchableEntity entityToPrepare)
        {
            var convertTypeMethod = _convertTypeToPrecise.PreciseTypeConfiguration[entityToPrepare.ValueType];
            
            entityToPrepare.ValueToSearch = convertTypeMethod(entityToPrepare.ValueToSearch);
            if (entityToPrepare.AdditionalValue != null)
            {
                entityToPrepare.AdditionalValue = convertTypeMethod(entityToPrepare.AdditionalValue);
            }

            return entityToPrepare;
        }
    }
}