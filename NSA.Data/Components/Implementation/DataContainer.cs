using System.Collections.Generic;

namespace NSA.Data.Components.Implementation
{
    public class DataContainer : IDataContainer
    {
        private readonly Dictionary<string, object> _data = new Dictionary<string, object>();

        public bool TryGetValue<TObject>(string key, out TObject value)
        {
            object v;
            
            if (this._data.TryGetValue(key, out v))
            {
                value = (TObject) v;
                return true;
            }

            value = default(TObject);
            return false;
        }

        public bool Add<TObject>(string key, TObject value)
        {
            if (this._data.ContainsKey(key))
                return false;

            this._data.Add(key, value);
            return true;
        }
    }
}
