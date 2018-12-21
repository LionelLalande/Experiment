using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace GraphDemo
{
    internal static class Program
    {
        private static void Main()
        {
            var g = new Graph<double>();

            var α = g.CreateVertex("α");
            var A = g.CreateVertex("A");
            var B = g.CreateVertex("B");
            var C = g.CreateVertex("C");
            var D = g.CreateVertex("D");
            var E = g.CreateVertex("E");
            var ω = g.CreateVertex("ω");

            g.Connect("α", "A", 3);
            g.Connect("α", "C", 2);
            g.Connect("α", "E", 6);
            g.Connect("A", "B", 6);
            g.Connect("A", "D", 1);
            g.Connect("B", "ω", 1);
            g.Connect("C", "A", 2);
            g.Connect("C", "D", 3);
            g.Connect("D", "ω", 4);
            g.Connect("A", "D", 1);
            g.Connect("E", "ω", 2);


            var shortestPath = new ShortestPath();
            var result = shortestPath.GetShortestPath<double>(g, "α", "ω", e => e.Value);

            ////{
            ////    var path = g.GetLongestPath("α", "ω");
            ////    var weight = path.Sum(e => e.Weight);
            ////}
        }
    }
}
