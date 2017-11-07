using System;
using System.Collections.Generic;

namespace GenericSearch.Helpers
{
    public interface IConvertTypeToPrecise
    {
        IDictionary<Type, Func<object, object>> PreciseTypeConfiguration { get; set; }
        
        T GetPreciseTypeValue<T>(object valueToConvert);
    }
}