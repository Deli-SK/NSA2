using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using JetBrains.Annotations;

using NSA.Data.Components;
using NSA.Data.Components.Implementation;

namespace NSA.Data
{
    public class Graph
    {
        private readonly ObservableCollection<Node> _nodes = new ObservableCollection<Node>();
        private readonly ObservableCollection<Connection> _connections = new ObservableCollection<Connection>();

        public ReadOnlyObservableCollection<Node> Nodes { get; }
        public ReadOnlyObservableCollection<Connection> Connections { get; }

        public Graph()
        {
            this.Connections = new ReadOnlyObservableCollection<Connection>(this._connections);
            this.Nodes = new ReadOnlyObservableCollection<Node>(this._nodes);
        }

        public Node AddNode(IDataContainer data = null)
        {
            var node = new Node(data);

            this._nodes.Add(node);
            return node;
        }

        public void RemoveNode(Node node)
        {
            this._nodes.Remove(node);

            foreach (var connection in this._connections.Where(c => c.From.Equals(node) || c.To.Equals(node)).ToArray())
            {
                this._connections.Remove(connection);
            }
        }

        public void AddConnection([NotNull]Node from, [NotNull]Node to, [CanBeNull] IDataContainer data = null)
        {
            Contract.Requires(from != null);
            Contract.Requires(to != null);

            var connection = from.AddConnection(to, data);
            this._connections.Add(connection);
        }
        
        public void RemoveConnection(Connection connection)
        {
            this._connections.Remove(connection);

            connection.From.RemoveConnection(connection);
        }

        public void Clear()
        {
            this._connections.Clear();
            this._nodes.Clear();
        }
    }
}