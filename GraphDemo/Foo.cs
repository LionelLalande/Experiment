using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace GraphDemo
{
    public sealed class ShortestPathResult<T>
    {
        internal static ShortestPathResult<T> Empty = new ShortestPathResult<T>(new IEdge<T>[0]);
        public IEnumerable<IVertex> Vertices { get; }
        public IReadOnlyList<IEdge<T>> Path { get; }
        internal ShortestPathResult(IReadOnlyList<IEdge<T>> path)
        {
            Path = path;
            var vertices = new List<IVertex>();
            if (path.Count > 0)
            {
                vertices.Add(path[0].Source);
            }
            foreach (var edge in path)
            {
                vertices.Add(edge.Target);
            }
            Vertices = vertices;
        }
    }
    public interface IShortestPath
    {
        ShortestPathResult<T> GetShortestPath<T>(Graph<T> graph, string source, string target, Func<IEdge<T>, double> weightFunc);
        ShortestPathResult<T> GetShortestPath<T>(Graph<T> graph, string source, string target, Func<IEdge<T>, double> weightFunc, Comparison<T> comparer);
        ShortestPathResult<T> GetShortestPath<T>(Graph<T> graph, string source, string target, Func<IEdge<T>, double> weightFunc, IComparer<T> comparer);
    }
    public class ShortestPath : IShortestPath
    {
        public ShortestPathResult<T> GetShortestPath<T>(Graph<T> graph, string source, string target, Func<IEdge<T>, double> weightFunc) =>
            GetShortestPath(graph, source, target, weightFunc, Comparer<T>.Default);
        public ShortestPathResult<T> GetShortestPath<T>(Graph<T> graph, string source, string target, Func<IEdge<T>, double> weightFunc, Comparison<T> comparer)
        {
            if (weightFunc == null) throw new ArgumentNullException(nameof(weightFunc));

            var vertices = graph.Vertices;
            vertices.TryGetValue(new Vertex(source.ToString()), out var sourceVertex);
            vertices.TryGetValue(new Vertex(target.ToString()), out var targetVertex);

            if (sourceVertex == null) throw new KeyNotFoundException("Cannot find source");
            if (targetVertex == null) throw new KeyNotFoundException("Cannot find target");

            if (sourceVertex == targetVertex) return ShortestPathResult<T>.Empty;

            var priorityQueue = new List<IVertex>(vertices.Count);
            var distances = new Dictionary<IVertex, double>();
            var previous = new Dictionary<IVertex, IEdge<T>>(vertices.Count);

            Prepare();
            ComputeDistances();
            return new ShortestPathResult<T>(ComputePath());

            void Prepare()
            {
                foreach (var vertex in vertices)
                {
                    distances[vertex] = double.PositiveInfinity;
                    priorityQueue.Add(vertex);
                }

                distances[sourceVertex] = 0;
            }

            void ComputeDistances()
            {
                IVertex vertex;
                do
                {
                    priorityQueue.Sort((x, y) => distances[x] - distances[y] <= 0 ? -1 : 1);
                    vertex = priorityQueue[0];
                    priorityQueue.Remove(vertex);

                    if (vertex == targetVertex)
                    {
                        // Exit early if already found the samllest path!
                        break;
                    }

                    foreach (IEdge<T> edge in vertex.Neighbors)
                    {
                        var alt = distances[vertex] + weightFunc(edge);
                        if (alt < distances[edge.Target])
                        {
                            distances[edge.Target] = alt;
                            previous[edge.Target] = edge;
                        }
                    }
                }
                while (priorityQueue.Count > 0);
            }

            List<IEdge<T>> ComputePath()
            {
                var node = targetVertex;
                var path = new List<IEdge<T>>(vertices.Count);
                while (previous.ContainsKey(node))
                {
                    path.Add(previous[node]);
                    node = previous[node].Source;
                }
                path.Reverse();
                return path;
            }
        }
        public ShortestPathResult<T> GetShortestPath<T>(Graph<T> graph, string source, string target, Func<IEdge<T>, double> weightFunc, IComparer<T> comparer) =>
            GetShortestPath(graph, source, target, weightFunc, comparer.Compare);
    }
    public interface IReadOnlySet<T> : IReadOnlyCollection<T>
    {
        bool TryGetValue(T item, out T result);
    }
    public sealed class ReadOnlySet<T> : IReadOnlySet<T>
    {
        public int Count { get => _inner.Count; }
        public ReadOnlySet(HashSet<T> inner)
        {
            _inner = inner;
        }
        public bool TryGetValue(T item, out T result)
        {
            return _inner.TryGetValue(item, out result);
        }
        public IEnumerator<T> GetEnumerator() => _inner.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => _inner.GetEnumerator();
        private readonly HashSet<T> _inner;
    }
    public sealed class Graph<TEdge>
    {
        public IReadOnlySet<IVertex> Vertices { get => new ReadOnlySet<IVertex>(_vertices); }
        public IReadOnlySet<IEdge<TEdge>> Edges { get => new ReadOnlySet<IEdge<TEdge>>(_edges); }
        public IVertex CreateVertex(string id)
        {
            IVertex vertex = new Vertex(id);
            if (_vertices.TryGetValue(vertex, out var tmp))
            {
                vertex = tmp;
            }
            else
            {
                _vertices.Add(vertex);
            }
            return vertex;
        }
        public IEdge<TEdge> Connect(string sourceId, string targetId) =>
            Connect(sourceId, targetId, default(TEdge));
        public IEdge<TEdge> Connect(string sourceId, string targetId, TEdge value)
        {
            _vertices.TryGetValue(new Vertex(sourceId), out var sourceVertex);
            _vertices.TryGetValue(new Vertex(targetId), out var targetVertex);

            IEdge<TEdge> edge = new Edge<TEdge>(sourceVertex, targetVertex, value);
            if (_edges.TryGetValue(edge, out var tmp))
            {
                edge = tmp;
            }
            else
            {
                _edges.Add(edge);
                sourceVertex.Add(edge);
            }
            return edge;
        }
        private readonly HashSet<IVertex> _vertices = new HashSet<IVertex>(VertexComparer.Default);
        private readonly HashSet<IEdge<TEdge>> _edges = new HashSet<IEdge<TEdge>>(EdgeComparer<TEdge>.Default);
    }
    public sealed class VertexComparer : IEqualityComparer<IVertex>
    {
        public static readonly VertexComparer Default = new VertexComparer();
        public bool Equals(IVertex x, IVertex y) =>
            string.Equals(x.Id, y.Id, StringComparison.OrdinalIgnoreCase);
        public int GetHashCode(IVertex obj) =>
            obj.GetHashCode();
    }
    public sealed class Vertex : IVertex
    {
        public string Id { get; }
        public IReadOnlyCollection<IEdge> Neighbors { get => _neighbors; }
        public Vertex(string id)
        {
            Id = id;
        }
        public void Add(IEdge edge)
        {
            _neighbors.Add(edge);
        }
        public override int GetHashCode() => Id.GetHashCode();
        public override string ToString() => $"{Id} [neighbors: {_neighbors.Count}]";
        private readonly List<IEdge> _neighbors = new List<IEdge>();
    }
    public interface IVertex
    {
        string Id { get; }
        IReadOnlyCollection<IEdge> Neighbors { get; }
        void Add(IEdge edge);
    }
    public sealed class EdgeComparer<T> : IEqualityComparer<IEdge<T>>
    {
        public static readonly EdgeComparer<T> Default = new EdgeComparer<T>();
        public bool Equals(IEdge<T> x, IEdge<T> y) =>
            VertexComparer.Default.Equals(x.Source, y.Source) &&
            VertexComparer.Default.Equals(x.Target, y.Target);
        public int GetHashCode(IEdge<T> obj) =>
            obj.GetHashCode();
    }
    public sealed class Edge<T> : IEdge<T>
    {
        public IVertex Source { get; }
        public IVertex Target { get; }
        public T Value { get; }
        public Edge(IVertex source, IVertex target, T value)
        {
            Source = source;
            Target = target;
            Value = value;
        }
        public override int GetHashCode()
        {
            var h = 17;
            h = ((h << 27) | h) ^ Source.GetHashCode();
            h = ((h << 27) | h) ^ Target.GetHashCode();
            return h;
        }
        public override string ToString() => $"{Source.Id} -> {Target.Id} ({Value})";
    }
    public interface IEdge<T> : IEdge
    {
        T Value { get; }
    }
    public interface IEdge
    {
        IVertex Source { get; }
        IVertex Target { get; }
    }
    //    public IReadOnlyCollection<Edge> GetLongestPath(object fromId, object toId)
    //    {
    //        _nodes.TryGetValue(new Node(fromId.ToString()), out var from);
    //        _nodes.TryGetValue(new Node(toId.ToString()), out var to);

    //        if (from == null) throw new Exception();
    //        if (to == null) throw new Exception();

    //        if (from == to) return EmptyPath;

    //        var priorityQueue = new List<Node>(_nodes.Count);
    //        var distances = new Dictionary<Node, double>();
    //        var previous = new Dictionary<Node, Edge>(_nodes.Count);

    //        Prepate();
    //        ComputeDistances();
    //        return ComputePath();

    //        void Prepate()
    //        {
    //            foreach (var node in _nodes)
    //            {
    //                distances[node] = double.NegativeInfinity;
    //                priorityQueue.Add(node);
    //            }

    //            distances[from] = 0;
    //        }

    //        void ComputeDistances()
    //        {
    //            Node node;
    //            do
    //            {
    //                priorityQueue.Sort((x, y) => distances[x] - distances[y] < 0 ? 1 : -1);
    //                node = priorityQueue[0];
    //                priorityQueue.Remove(node);

    //                foreach (var edge in node.Edges.ToArray())
    //                {
    //                    var alt = distances[node] + edge.Weight;
    //                    if (alt > distances[edge.ToNode])
    //                    {
    //                        distances[edge.ToNode] = alt;
    //                        previous[edge.ToNode] = edge;
    //                    }
    //                }
    //            }
    //            while (priorityQueue.Count > 0);
    //        }

    //        List<Edge> ComputePath()
    //        {
    //            var node = to;
    //            var path = new List<Edge>(_nodes.Count);
    //            while (previous.ContainsKey(node))
    //            {
    //                path.Add(previous[node]);
    //                node = previous[node].FromNode;
    //            }
    //            path.Reverse();
    //            return path;
    //        }
    //    }

    //    private static readonly List<Edge> EmptyPath = new List<Edge>(0);
    //    private readonly object _syncObj = new object();
    //    private readonly HashSet<Node> _nodes;
    //    private readonly Dictionary<string, int[]> _indices = new Dictionary<string, int[]>();
    //}
}
