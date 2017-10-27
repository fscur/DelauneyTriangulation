﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriangleLib
{
    //planar straigh line graph
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

        public Edge Find(Vertex v0, Vertex v1)
        {
            return _edges.FirstOrDefault(e => (e.V0 == v0 && e.V1 == v1) || (e.V0 == v1 && e.V1 == v0));
        }
    }
}