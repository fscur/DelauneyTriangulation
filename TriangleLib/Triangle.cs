﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriangleLib
{
    public class Triangle
    {
        public Vertex V0;
        public Vertex V1;
        public Vertex V2;
        public Edge E0;
        public Edge E1;
        public Edge E2;

        public Triangle(Edge e0, Edge e1, Edge e2)
        {
            E0 = e0;
            E1 = e1;
            E2 = e2;
            
            var a = e0.V0 == e1.V0 || e0.V0 == e1.V1 ? e0.V0 : e0.V1;
            var b = e1.V0 == e2.V0 || e1.V0 == e2.V1 ? e1.V0 : e1.V1;
            var c = e2.V0 == e0.V0 || e2.V0 == e0.V1 ? e2.V0 : e2.V1;

            var isCounterClockwise = Vec2.Cross(b.Position - a.Position, c.Position - a.Position) > 0;

            V0 = a;
            V1 = isCounterClockwise ? b : c;
            V2 = isCounterClockwise ? c : b;

            E0.Triangles.Add(this);
            E1.Triangles.Add(this);
            E2.Triangles.Add(this);
        }

        public Triangle(Vertex a, Vertex b, Vertex c)
        {
            var isCounterClockwise = Vec2.Cross(b.Position - a.Position, c.Position - a.Position) > 0;

            V0 = a;
            V1 = isCounterClockwise ? b : c;
            V2 = isCounterClockwise ? c : b;

            //find for edges in the already present edges of the current vertices
            var e0 = V0.Find(V1);
            var e1 = V1.Find(V2);
            var e2 = V2.Find(V0);

            if (e0 == null)
            {
                E0 = new Edge(V0, V1);
                V0.AddEdge(E0);
                V1.AddEdge(E0);
            }
            else
                E0 = e0;

            if (e1 == null)
            {
                E1 = new Edge(V1, V2);
                V1.AddEdge(E1);
                V2.AddEdge(E1);
            }
            else
                E1 = e1;

            if (e2 == null)
            {
                E2 = new Edge(V2, V0);
                V2.AddEdge(E2);
                V0.AddEdge(E2);
            }
            else
                E2 = e2;

            E0.Triangles.Add(this);
            E1.Triangles.Add(this);
            E2.Triangles.Add(this);
        }
        
        public Triangle(Vertex a, Vertex b)
        {
            V0 = a;
            V1 = b;

            //find for edges in the already present edges of the current vertices
            var e0 = V0.Find(V1);

            if (e0 == null)
            {
                E0 = new Edge(V0, V1);
                V0.AddEdge(E0);
                V1.AddEdge(E0);
            }
            else
                E0 = e0;

            E0.Triangles.Add(this);
        }
        
        public static bool IsDegenerate(Triangle t)
        {
            if (t.V0 == null | t.V1 == null | t.V2 == null)
                return true;

            var cross = Vec2.Cross(t.V1.Position - t.V0.Position, t.V2.Position - t.V0.Position);
            return Compare.AlmostEqual(cross, 0.0, 0.5e-2);
        }

        public static bool CircumcircleContainsPoint(Triangle t, Vec2 p)
        {
            return CircumcircleContainsPoint(t.V0.Position, t.V1.Position, t.V2.Position, p);
        }

        public static bool CircumcircleContainsPoint(Vec2 a, Vec2 b, Vec2 c, Vec2 d)
        {
            if (Compare.Less(Vec2.Cross(b - a, c - a), 0, Compare.TOLERANCE))
            {
                var t = b;
                b = c;
                c = t;
            }

            double adx, ady, bdx, bdy, cdx, cdy, dx, dy, dnorm;

            dx = d.X;
            dy = d.Y;
            dnorm = Vec2.SquaredLength(d);
            adx = a.X - dx;
            ady = a.Y - dy;
            bdx = b.X - dx;
            bdy = b.Y - dy;
            cdx = c.X - dx;
            cdy = c.Y - dy;

            var det = (
                (Vec2.SquaredLength(a) - dnorm) * (bdx * cdy - bdy * cdx) +
                (Vec2.SquaredLength(b) - dnorm) * (cdx * ady - cdy * adx) +
                (Vec2.SquaredLength(c) - dnorm) * (adx * bdy - ady * bdx));

            var containsPoint = Compare.Greater(det, 0.0, Compare.TOLERANCE);

            return containsPoint;
        }
        
        public override string ToString()
        {
            var v0 = V0 != null ? V0.ToString() : string.Empty;
            var v1 = V1 != null ? V1.ToString() : string.Empty;
            var v2 = V2 != null ? V2.ToString() : string.Empty;

            return "[" + v0 + "; " + v1 + "; " + v2 + "]";
        }

        public bool Contains(Edge edge)
        {
            return edge == E0 || edge == E1 || edge == E2;
        }
    }
}

