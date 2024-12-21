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

    public int Numerator => numerator % denominator;

    public int Denominator => denominator;

    public static Angle operator +(Angle a1, Angle a2)
    {
        int new_num = a1.Numerator + a2.Numerator;
        new_num %= a1.Denominator;
        return new Angle(new_num, a1.Denominator);
    }

    public double ToDouble()
    {
        double n = numerator;
        double d = denominator;
        return Math.PI * 2 * n / d;
    }

    public static bool operator ==(Angle a1, Angle a2)
    {
        return a1.GetHashCode() == a2.GetHashCode();
    }

    public static bool operator !=(Angle a1, Angle a2)
    {
        return a1.GetHashCode() != a2.GetHashCode();
    }

    public override bool Equals(object? obj)
    {
        return obj != null && obj is Angle angle && (Numerator % Denominator) == (angle.Numerator % angle.Denominator);
    }

    public override int GetHashCode()
    {
        return (numerator % denominator).GetHashCode();
    }
}
