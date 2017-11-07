using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace GenericSearch.Helpers
{
    public class PreciseTypeConverter : IConvertTypeToPrecise
    {
        public IDictionary<Type, Func<object, object>> PreciseTypeConfiguration { get; set; }
        
        public T GetPreciseTypeValue<T>(object valueToConvert)
        {
            var genericParameterType = typeof(T);

            var preciseType = Nullable.GetUnderlyingType(genericParameterType) ?? genericParameterType;

            return valueToConvert == null ? default(T) : (T)Convert.ChangeType(valueToConvert, preciseType);
        }

        public PreciseTypeConverter()
        {
            PreciseTypeConfiguration = new Dictionary<Type, Func<object, object>>()
            {
                {typeof(decimal), (value) => GetPreciseTypeValue<decimal>(value)},
                {typeof(decimal?), (value) => GetPreciseTypeValue<decimal?>(value)},
                {typeof(float), (value) => GetPreciseTypeValue<float>(value)},
                {typeof(float?), (value) => GetPreciseTypeValue<float?>(value)},
                {typeof(int), (value) => GetPreciseTypeValue<int>(value)},
                {typeof(int?), (value) => GetPreciseTypeValue<int?>(value)},
                {typeof(string), (value) => GetPreciseTypeValue<string>(value)},
                {typeof(DateTime), (value) => GetPreciseTypeValue<DateTime>(value)},
                {typeof(DateTime?), (value) => GetPreciseTypeValue<DateTime?>(value)}
            };
        }
    }
}