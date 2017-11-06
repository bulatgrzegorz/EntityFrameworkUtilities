namespace GenericSearch.Expressions
{
    public enum SearchClauseStrategy
    {
        Normal = 0,
        ByContains = 1,
        ByRange = 2,
        CombiningByOr = 3
    }
}