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
        var datasetLength = (int)Ioc.Resolve<int>("Collision.Dataset.Length");
        var rawPolygon1 = (Polygon)shapesDict[shape1];
        var polygon2 = (Polygon)shapesDict[shape2];

        return Enumerable.Range(0, datasetLength)
                        .Select(i =>
                        {
                            var point0 = CreatePoint();
                            var velocity = CreatePoint();

                            // Собираем список уникальных точек из кортежей
                            List<Point> points = rawPolygon1.Edges()
                                                        .SelectMany(edge => new[] { edge.start, edge.end }) // Раскрываем пары в отдельные точки
                                                        .Distinct()                                          // Удаляем дублирующиеся точки
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

// public class CollisionDataGenerator
// {
//     private readonly Random rnd;

//     public CollisionDataGenerator()
//     {
//         rnd = new Random();
//     }

//     public IList<int[]> GenerateCollisionData() {
//         List<int[]> collisionDataList = new List<int[]>();
//         var datasetLength = Ioc.Resolve<int>("Collision.Dataset.Length");
//         var pointCount1 = Ioc.Resolve<int>("Collision.Dataset.PointCount.FirstShape");
//         var pointCount2 = Ioc.Resolve<int>("Collision.Dataset.PointCount.SecondShape");
//         for (int i = 0; i < datasetLength; i++) 
//         {
//             var polygon1 = CreateShape(pointCount1);
//             var polygon2 = CreateShape(pointCount2);
//             var velocity = CreateVelocity();

//             var collisions = CollisionDetector.FindIntersections(polygon1, polygon2, velocity);
//             if (collisions.Capacity > 0)
//             {
//                 var collisionLine = new List<int> {};
//                 foreach (Polygon polygon in new Polygon[] {polygon1, polygon2}) {
//                     foreach (Point p in polygon.Points) {
//                         collisionLine.Add((int) p.X);
//                         collisionLine.Add((int) p.Y);
//                     }
//                 }

//                 collisionLine.Add((int) velocity.X);
//                 collisionLine.Add((int) velocity.Y);
//                 collisionDataList.Add(collisionLine.ToArray<int>());
//             }
//         }

//         return collisionDataList;
//     }

//     public Polygon CreateShape(int anglesCount) {
//         var fieldWidth = Ioc.Resolve<int>("Field.Width");
//         var fieldHeight = Ioc.Resolve<int>("Field.Height");

//         List<Point> points = new List<Point>();

//         for (int i = 0; i < anglesCount; i++) 
//         {
//             points.Add(new Point(rnd.Next(0, fieldWidth), rnd.Next(0, fieldHeight)));
//         }

//         return new Polygon(points);
//     }

//     public Point CreateVelocity()
//     {
//         var quadrantSize = Ioc.Resolve<int>("Field.Quadrant.Size");
//         return new Point(rnd.Next(0, quadrantSize), rnd.Next(0, quadrantSize));
//     }
// }
