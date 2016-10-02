using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using JetBrains.Annotations;

namespace NSA.Data.Components.Implementation
{
    public class Node : IDataContainer
    {
        private readonly List<Connection> _connections = new List<Connection>();
        
        public ReadOnlyCollection<Connection> Connections { get; } 

        private readonly IDataContainer _data;

        internal Node(IDataContainer data = null)
        {
            this._data = data ?? new DataContainer();
            this.Connections = new ReadOnlyCollection<Connection>(this._connections);
        }

        internal Connection AddConnection([NotNull] Node to, IDataContainer data = null)
        {
            var connection = new Connection(this, to, data ?? new DataContainer());
            this._connections.Add(connection);
            return connection;
        }

        public bool HasConnection(Connection connection)
        {
            return this._connections.Contains(connection);
        }

        public bool HasConnection(Node to)
        {
            return this._connections.Any(c => c.To.Equals(to));
        }

        internal void RemoveConnection(Connection connection)
        {
            this._connections.Remove(connection);
        }

        #region IDataContainer Implementation

        public bool TryGetValue<TObject>(string key, out TObject value)
        {
            return this._data.TryGetValue(key, out value);
        }

        public bool Add<TObject>(string key, TObject value)
        {
            return this._data.Add(key, value);
        }

        #endregion
    }
}