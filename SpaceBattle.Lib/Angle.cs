namespace SpaceBattle.Lib;

public class Angle
{
    private readonly int numerator;
    private readonly int denominator;

    public Angle(int n, int d)
    {
        numerator = n % d;
        denominator = d;
    }

    public int Numerator => numerator;

    public int Denominator => denominator;

    public static Angle operator +(Angle a1, Angle a2)
    {
        int new_num = a1.Numerator + a2.Numerator;
        return new Angle(new_num, a1.Denominator);
    }

    public static implicit operator double(Angle a)
    {
        double n = a.Numerator;
        double d = a.Denominator;
        return n / d * Math.PI * 2;
    }

    public static bool operator ==(Angle a1, Angle a2)
    {
        return ReferenceEquals(a1, a2);
    }

    public static bool operator !=(Angle a1, Angle a2)
    {
        return !ReferenceEquals(a1, a2);
    }

    public override bool Equals(object? obj)
    {
        return obj != null && obj is Angle angle && Numerator == angle.Numerator;
    }

    public override int GetHashCode()
    {
        return numerator.GetHashCode();
    }
}
