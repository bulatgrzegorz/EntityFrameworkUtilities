using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace GenericSearch.Helpers
{
    public class PreciseTypeConverter : IConvertTypeToPrecise
    {
        public IDictionary<Type, Action<Type, object>> PreciseTypeConfiguration { get; set; }
        
        public T GetPreciseTypeValue<T>(object valueToConvert)
        {
            var genericParameterType = typeof(T);

            var preciseType = Nullable.GetUnderlyingType(genericParameterType) ?? genericParameterType;

            return valueToConvert == null ? default(T) : (T)Convert.ChangeType(valueToConvert, preciseType);
        }

        public PreciseTypeConverter()
        {
            PreciseTypeConfiguration = new Dictionary<Type, Action<Type, object>>()
            {
                {typeof(decimal), (type, value) => GetPreciseTypeValue<decimal>(value)},
                {typeof(decimal?), (type, value) => GetPreciseTypeValue<decimal?>(value)},
                {typeof(float), (type, value) => GetPreciseTypeValue<float>(value)},
                {typeof(float?), (type, value) => GetPreciseTypeValue<float?>(value)},
                {typeof(int), (type, value) => GetPreciseTypeValue<int>(value)},
                {typeof(int?), (type, value) => GetPreciseTypeValue<int?>(value)},
                {typeof(string), (type, value) => GetPreciseTypeValue<string>(value)},
                {typeof(DateTime), (type, value) => GetPreciseTypeValue<DateTime>(value)},
                {typeof(DateTime?), (type, value) => GetPreciseTypeValue<DateTime?>(value)}
            };
        }
    }
}