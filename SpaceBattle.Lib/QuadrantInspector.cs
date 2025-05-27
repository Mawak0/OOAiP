using App;

namespace SpaceBattle.Lib;

public class QuadrantInspector
{
    private readonly object quadrantSize;
    private readonly Dictionary<(int, int), List<string>> quadrantField;

    public QuadrantInspector()
    {
        quadrantSize = Ioc.Resolve<int>("Field.Quadrant.Size");
        quadrantField = new Dictionary<(int, int), List<string>>();
    }

    public Dictionary<(int, int), List<string>> QuadrantField => quadrantField;

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

    public void AddObjectToField(IColliding obj)
    {
        var quadrants = getQuadrants(obj);

        quadrants.ToList().ForEach(q =>
        {
            var key = (q[0], q[1]);
            if (!quadrantField.TryGetValue(key, out var shapes))
            {
                quadrantField[key] = new List<string>();
            }

            quadrantField[key].Add(obj.Shape);
        });
    }

    public void DeleteObjectFromField(IColliding obj)
    {
        var quadrants = getQuadrants(obj);

        quadrants.Where(q => quadrantField.ContainsKey((q[0], q[1]))).ToList()
            .ForEach(q => quadrantField[(q[0], q[1])].Remove(obj.Shape));
    }

    public void UpdateObjectOnField(IColliding obj, IColliding newObj)
    {
        DeleteObjectFromField(obj);
        AddObjectToField(newObj);
    }

    public List<IColliding> GetObjectsInSameSquare(int[] quadrantCoords)
    {
        var figuresInQuadrant = quadrantField[(quadrantCoords[0], quadrantCoords[1])];
        var collidingDict = Ioc.Resolve<Dictionary<string, IColliding>>("Collision.GetCollisionObjectsDict");

        return figuresInQuadrant.Select(key => collidingDict[key]).ToList();
    }
}
