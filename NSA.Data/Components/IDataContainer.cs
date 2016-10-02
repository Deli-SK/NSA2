namespace NSA.Data.Components
{
    public interface IDataContainer
    {
        bool TryGetValue<TObject>(string key, out TObject value);
        bool Add<TObject>(string key, TObject value);
    }
}