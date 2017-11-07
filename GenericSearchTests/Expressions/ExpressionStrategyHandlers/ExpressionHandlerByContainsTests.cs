using System;
using GenericSearch;
using GenericSearch.Expressions;
using NUnit.Framework;
using GenericSearch.Expressions.ExpressionStrategyHandlers;

namespace GenericSearchTests.Expressions.ExpressionStrategyHandlers
{
    [TestFixture]
    public class ExpressionHandlerByContainsTests
    {
        private ExpressionHandlerByContains _expressionHandlerByContains;

        [SetUp]
        public void SetUp()
        {
            _expressionHandlerByContains = new ExpressionHandlerByContains();
        }

        [Test]
        public void CreateExpression_ThrowArgumentExceptionWhenEntityValueTypeIsNotString()
        {
            var testEntity = new TestEntity() {ValueType = typeof(int)};

            Assert.Throws<ArgumentException>(
                () => _expressionHandlerByContains.CreateExpression<int>(testEntity, null));
        }
        
        private class TestEntity : ISearchableEntity
        {
            public string ColumnNameToSearchBy { get; set; }
            public object ValueToSearch { get; set; }
            public object AdditionalValue { get; set; }
            public SearchClauseStrategy SearchStrategy { get; set; }
            public Type ValueType { get; set; }
        }
    }
}