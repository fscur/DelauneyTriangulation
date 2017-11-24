﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriangleLib
{
    //planar straight line graph
    public class PSLG
    {
        private List<Edge> _edges;
        public List<Edge> Edges
        {
            get { return _edges; }
        }

        public PSLG()
        {
            _edges = new List<Edge>();
        }

        public bool Contains(Edge edge)
        {
            return Find(edge.V0, edge.V1) != null;
        }

        public Edge Find(Edge edge)
        {
            return Find(edge.V0, edge.V1);
        }

        public Edge Find(Vertex v0, Vertex v1)
        {
            return _edges.FirstOrDefault(e => (e.V0.Position == v0.Position && e.V1.Position == v1.Position) || (e.V0.Position == v1.Position && e.V1.Position == v0.Position));
        }

        public void RemoveEdge(Edge edge)
        {
            var e = Find(edge);
            if (e != null)
                Edges.Remove(e);
        }

        public void AddEdge(Edge edge)
        {
            if (!Contains(edge))
                Edges.Add(edge);
        }
    }
}
