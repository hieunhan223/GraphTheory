using System;
using System.Collections.Generic;
using System.Text;

namespace GraphTheory2022
{
    internal class Edge : IEquatable<Edge>
    {
        public int start;    // dinh bat dau
        public int end;      // dinh ket thuc
        public int weight;  // trong so 
        
        public Edge(int start_vertex, int end_vertex, int edge_weight)
        {
            start = start_vertex;
            end = end_vertex;
            weight = edge_weight;
        }
        public bool Equals(Edge other)
        {
            return !(other is null) &&
                   start == other.start &&
                   end == other.end &&
                   weight == other.weight;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(start, end, weight);
        }
    }
}
