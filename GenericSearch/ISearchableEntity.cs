using System;
using GenericSearch.Expressions;

namespace GenericSearch
{
    public interface ISearchableEntity
    {
        string ColumnNameToSearchBy { get; set; }
        
        object ValueToSearch { get; set; }
        
        object AdditionalValue { get; set; }
        
        SearchClauseStrategy SearchStrategy { get; set; }
        
        Type ValueType { get; set; } 
    }
}