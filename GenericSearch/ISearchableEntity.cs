using System;

namespace GenericSearch
{
    public interface ISearchableEntity
    {
        string ColumnNameToSearchBy { get; set; }
        
        object ValueToSearch { get; set; }
        
        object AdditionalValue { get; set; }
        
        bool IsSearchByContains { get; set; }
        
        Type ValueType { get; set; } 
    }
}