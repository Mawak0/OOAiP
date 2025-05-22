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
        foreach (int[] el in list)
        {
            if (el[0] == elem[0] && el[1] == elem[1])
            {
                return true;
            }
        }

        return false;
    }

    public List<int[]> getQuadrants(IColliding obj)
    {
        var shapesDict = (IDictionary<string, Polygon>)Ioc.Resolve<object>("Collision.GetPolygonDict");
        var polygon = shapesDict[obj.Shape];
        List<int[]> quadrants = new List<int[]>();

        foreach (Point point in polygon.Points)
        {
            var xCoord = (int)point.X / (int)quadrantSize;
            var yCoord = (int)point.Y / (int)quadrantSize;

            int[] qCoords = { xCoord, yCoord };
            if (!Contain(quadrants, qCoords))
            {
                quadrants.Add(qCoords);
            }
        }

        return quadrants;
    }

    public bool InQuadrant(IColliding obj, int[] quadrantCoords)
    {
        var quadrants = getQuadrants(obj);

        return Contain(quadrants, quadrantCoords);
    }

    public List<IColliding> GetObjectsInSameSquare(int[] quadrantCoords)
    {
        var collidingObjectsList = (IList<IColliding>)Ioc.Resolve<object>("Collision.GetCollisionObjectsList");
        List<IColliding> nearObjects = new List<IColliding>();
        foreach (IColliding cObj in collidingObjectsList)
        {
            var objectQuadrants = getQuadrants(cObj);
            if (Contain(objectQuadrants, quadrantCoords))
            {
                nearObjects.Add(cObj);
            }
        }

        return nearObjects;
    }
}
