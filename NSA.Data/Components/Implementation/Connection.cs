using System.Diagnostics.Contracts;
using JetBrains.Annotations;

namespace NSA.Data.Components.Implementation
{
    public class Connection: IDataContainer
    {
        private readonly IDataContainer _data;

        public Node From { get; }
        public Node To { get; }
        
        internal Connection([NotNull] Node from, [NotNull] Node to) : this(from, to, new DataContainer()) { }

        internal Connection([NotNull] Node from, [NotNull] Node to, [NotNull] IDataContainer data)
        {
            Contract.Requires(from != null);
            Contract.Requires(to != null);
            Contract.Requires(data != null);

            this.From = from;
            this.To = to;
            this._data = data;
        }
        

        #region IDataContainer Implementation

        public bool TryGetValue<TObject>(string key, out TObject value)
        {
            return ((IDataContainer) this._data).TryGetValue<TObject>(key, out value);
        }

        public bool Add<TObject>(string key, TObject value)
        {
            return ((IDataContainer) this._data).Add<TObject>(key, value);
        }

        #endregion

    }
}
