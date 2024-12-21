namespace SpaceBattle.Lib;

public class Vector
{
    private int[] values;
    private readonly int length;

    public Vector(int[] values)
    {
        this.values = values;
        length = values.Length;
    }

    public int[] Values
    {
        get => values;
        set => values = value;
    }

    public int Len => length;

    public int this[int index]
    {
        get => Values[index];
        set => Values[index] = value;
    }

    public static Vector operator +(Vector v1, Vector v2)
    {
        if (v1.Len != v2.Len)
        {
            throw new ArgumentException("Vectors must be of the same length");
        }

        var result = v1.Values.Zip(v2.Values, (a, b) => a + b).ToArray();
        return new Vector(result);
    }

    public override bool Equals(object? obj)
    {
        return obj != null && obj is Vector vector && values.SequenceEqual(vector.values);
    }

    public override int GetHashCode()
    {
        return Values.Aggregate(17, (current, value) => current * 23 + value.GetHashCode());
    }

    public static bool operator ==(Vector v1, Vector v2)
    {
        return v1.GetHashCode() == v2.GetHashCode();
    }

    public static bool operator !=(Vector v1, Vector v2)
    {
        return v1.GetHashCode() != v2.GetHashCode();
    }

    public override string ToString()
    {
        return $"[{string.Join(", ", Values)}]";
    }
}
