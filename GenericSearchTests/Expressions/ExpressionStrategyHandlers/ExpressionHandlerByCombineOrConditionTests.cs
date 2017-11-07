using System;
using System.Collections.Generic;
using GenericSearch;
using GenericSearch.Expressions;
using GenericSearch.Expressions.ExpressionStrategyHandlers;
using NUnit.Framework;

namespace GenericSearchTests.Expressions.ExpressionStrategyHandlers
{
    [TestFixture]
    public class ExpressionHandlerByCombineOrConditionTests
    {
        private ExpressionHandlerByCombineOrCondition _expressionHandler;

        [SetUp]
        public void SetUp()
        {
            _expressionHandler = new ExpressionHandlerByCombineOrCondition();
        }
        
        [Test]
        [TestCase(default (int))]
        [TestCase(default (string))]
        [TestCase(default (bool))]
        [TestCase(default (IDictionary<string, object>))]
        public void CreateExpression_ThrowsArgumentExceptionWhenTypeIsNotOfTypeCollectionString(object passedValue)
        {
            var testEntity = new TestClass()
            {
                ValueToSearch = passedValue
            };

            Assert.Throws<ArgumentException>(() => _expressionHandler.CreateExpression<int>(testEntity, null, null));
        }

        [Test]
        public void CreateExpression_DoesNotThrowException_WhenPassedValueIsListOfString()
        {
            Assert.DoesNotThrow(() => _expressionHandler.CreateExpression<int>(
                CreateTestClass(new List<string> {"123", "1111"}), 
                null, 
                null));
        }
        
        [Test]
        public void CreateExpression_DoesNotThrowException_WhenPassedValueArrayOfString()
        {
            Assert.DoesNotThrow(() => _expressionHandler.CreateExpression<int>(
                CreateTestClass(new[] {"123", "1111"}), 
                null, 
                null));
        }

        private static TestClass CreateTestClass(object valueOfEntity)
        {
            return  new TestClass()
            {
                ValueToSearch = valueOfEntity
            };
        }
        
        private class TestClass : ISearchableEntity
        {
            public string ColumnNameToSearchBy { get; set; }
            public object ValueToSearch { get; set; }
            public object AdditionalValue { get; set; }
            public SearchClauseStrategy SearchStrategy { get; set; }
            public Type ValueType { get; set; }
        }
    }
}