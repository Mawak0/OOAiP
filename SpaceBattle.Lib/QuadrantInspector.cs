using App;

namespace SpaceBattle.Lib;

public class QuadrantInspector
{
    private readonly object quadrantSize;

    public QuadrantInspector()
    {
        quadrantSize = Ioc.Resolve<int>("Field.Quadrant.Size");
    }

    public static bool Contain(List<int[]> list, int[] elem)
    {
        return list.Any(el => el.SequenceEqual(elem)); // Используется SequenceEqual для сравнения массивов
    }

    public List<int[]> getQuadrants(IColliding obj)
    {
        var shapesDict = (IDictionary<string, Polygon>)Ioc.Resolve<object>("Collision.GetPolygonDict");
        var polygon = shapesDict[obj.Shape];

        return polygon.Points
                .Select(point =>
                    (
                        (int)point.X / (int)quadrantSize,
                        (int)point.Y / (int)quadrantSize
                    ))
                .Distinct()
                .Select(tuple => new[] { tuple.Item1, tuple.Item2 })
                .ToList();
    }
    public bool InQuadrant(IColliding obj, int[] quadrantCoords)
    {
        var quadrants = getQuadrants(obj);
        return Contain(quadrants, quadrantCoords);
    }

    public List<IColliding> GetObjectsInSameSquare(int[] quadrantCoords)
    {
        var collidingObjectsList = (IList<IColliding>)Ioc.Resolve<object>("Collision.GetCollisionObjectsList");

        return collidingObjectsList.Where(
                  cObj => Contain(getQuadrants(cObj), quadrantCoords)
              ).ToList();
    }
}
