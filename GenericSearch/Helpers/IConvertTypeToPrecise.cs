namespace GenericSearch.Helpers
{
    public interface IConvertTypeToPrecise
    {
        T GetPreciseTypeValue<T>(object valueToConvert);
    }
}