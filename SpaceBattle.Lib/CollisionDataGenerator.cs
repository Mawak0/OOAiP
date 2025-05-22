using App;

namespace SpaceBattle.Lib;

public class CollisionDataGenerator : ICollisionDataGenerator
{
    private readonly string shape1;
    private readonly string shape2;
    private readonly Random rnd;

    public string firstShape => shape1;

    public string secondShape => shape2;

    public CollisionDataGenerator(string shape1, string shape2)
    {
        this.shape1 = shape1;
        this.shape2 = shape2;
        rnd = new Random();
    }

    public IList<int[]> GenerateCollisionData()
    {
        var shapesDict = (IDictionary<string, Polygon>)Ioc.Resolve<object>("Collision.GetPolygonDict");
        var datasetLength = Ioc.Resolve<int>("Collision.Dataset.Length");
        var rawPolygon1 = shapesDict[shape1];
        var polygon2 = shapesDict[shape2];

        return Enumerable.Range(0, datasetLength)
                        .Select(i =>
                        {
                            var point0 = CreatePoint();
                            var velocity = CreatePoint();

                            List<Point> points = rawPolygon1.Edges()
                                                        .SelectMany(edge => new[] { edge.start, edge.end })
                                                        .Distinct()
                                                        .ToList();

                            for (int j = 0; j < points.Count; j++)
                            {
                                points[j] += point0;
                            }

                            var polygon1 = new Polygon(points);

                            var collisions = CollisionDetector.FindIntersections(polygon1, polygon2, velocity);
                            if (collisions.Capacity > 0)
                            {
                                var collisionLine = new List<int>();

                                collisionLine.AddRange(
                                    new[] { polygon1, polygon2 }.SelectMany(p => p.Points).SelectMany(p => new int[] { (int)p.X, (int)p.Y }));

                                collisionLine.Add((int)velocity.X);
                                collisionLine.Add((int)velocity.Y);

                                return collisionLine.ToArray();
                            }

                            return null;
                        })
                        .Where(line => line != null)
                        .Cast<int[]>()
                        .ToList();
    }

    public Point CreatePoint()
    {
        var quadrantSize = Ioc.Resolve<int>("Field.Quadrant.Size");
        return new Point(rnd.Next(0, quadrantSize), rnd.Next(0, quadrantSize));
    }
}

