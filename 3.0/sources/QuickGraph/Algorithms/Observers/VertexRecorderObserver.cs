using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace QuickGraph.Algorithms.Observers
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="Vertex"></typeparam>
    /// <typeparam name="Edge"></typeparam>
    /// <reference-ref
    ///     idref="boost"
    ///     />
#if !SILVERLIGHT
    [Serializable]
#endif
    public sealed class VertexRecorderObserver<TVertex, TEdge> :
        IObserver<IVertexTimeStamperAlgorithm<TVertex, TEdge>>
        where TEdge : IEdge<TVertex>
    {
        private readonly IList<TVertex> vertices;
        public VertexRecorderObserver()
            : this(new List<TVertex>())
        { }

        public VertexRecorderObserver(IList<TVertex> vertices)
        {
            if (vertices == null)
                throw new ArgumentNullException("edges");
            this.vertices = vertices;
        }

        public IEnumerable<TVertex> Vertices
        {
            get
            {
                return this.vertices;
            }
        }

        public IDisposable Attach(IVertexTimeStamperAlgorithm<TVertex, TEdge> algorithm)
        {
            algorithm.DiscoverVertex += new VertexAction<TVertex>(algorithm_DiscoverVertex);
            return new DisposableAction(
                () => algorithm.DiscoverVertex -= new VertexAction<TVertex>(algorithm_DiscoverVertex)
                );
        }

        void algorithm_DiscoverVertex(TVertex v)
        {
            this.vertices.Add(v);
        }
    }
}
