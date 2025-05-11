namespace SpaceBattle.Lib;
using System.Collections.Generic;

public class CollisionDetector
{
    public static List<Point> FindIntersections(Polygon poly1, Polygon poly2, Point velocity)
    {
        var intersections = new List<Point>();

        foreach (var vertex in poly1.Points)
        {
            var ray = new Ray(vertex, velocity);

            foreach (var edge in poly2.Edges())
            {
                var intersection = RayIntersectsEdge(ray, edge.start, edge.end);

                if (intersection != null)
                {
                    intersections.Add(intersection);
                }
            }
        }

        return intersections.Distinct().ToList();
    }

    private static Point? RayIntersectsEdge(Ray ray, Point start, Point end)
    {
        var dir = ray.Direction;
        var diff = start - ray.Origin;
        var edgeDir = end - start;

        double det = Point.Cross(dir, edgeDir);
        if (Math.Abs(det) < 1e-7)
        {
            return null;
        }

        double t = Point.Cross(diff, edgeDir) / det;
        double u = Point.Cross(diff, dir) / det;

        if (t >= 0 && u >= 0 && u <= 1)
        {
            return ray.Origin + t * dir;
        }

        return null;
    }
}