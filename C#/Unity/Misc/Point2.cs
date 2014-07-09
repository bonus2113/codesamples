using System;
using UnityEngine;

[Serializable]
public struct Point2
{
	//Useful presets
    public static readonly Point2 one =     new Point2(1, 1);
	public static readonly Point2 zero = 	new Point2 (0, 0);
	public static readonly Point2 unitX = 	new Point2 (1, 0);
	public static readonly Point2 unitY = 	new Point2 (0, 1);
	
	public int x;
	public int y;

	public Point2 (int _x, int _y)
	{
		x = _x;
		y = _y;
	}

	//Ease converting between Point2 and Vector2
	public Point2(Vector2 _vec) 
		: this((int)_vec.x, (int)_vec.y)
	{

	}

    public Point2 RotateClockwise(int times = 1)
    {
        int index = times%4;

        switch (index)
        {
            case 0:     return this;
            case 1:     return new Point2(y, -x);
            case 2:     return -this;
            case 3:     return new Point2(-y, x);
            default:    return this;
        }           
    }

	//Define some operators
	public static Point2 operator +(Point2 p1, Point2 p2) 
	{
		return new Point2(p1.x + p2.x, p1.y + p2.y);
	}

	public static Point2 operator -(Point2 p1, Point2 p2) 
	{
		return new Point2(p1.x - p2.x, p1.y - p2.y);
	}

    public static Point2 operator -(Point2 p1)
    {
        return new Point2(-p1.x, -p1.y);
    }

	public static Point2 operator *(Point2 p1, Point2 p2) 
	{
		return new Point2(p1.x * p2.x, p1.y * p2.y);
	}

    public static Point2 operator *(Point2 p1, int s)
    {
        return new Point2(p1.x * s, p1.y * s);
    }

    public static Point2 operator /(Point2 p1, int s)
    {
        return new Point2(p1.x / s, p1.y / s);
    }

	public static bool operator ==(Point2 p1, Point2 p2) 
	{
		return p1.x == p2.x && p1.y == p2.y;
	}

	public static bool operator !=(Point2 p1, Point2 p2) 
	{
		return p1.x != p2.x || p1.y != p2.y;
	}

	public override int GetHashCode ()
	{
        var hc = 0;
        hc = x.GetHashCode();
        hc = (hc << 3) ^ y.GetHashCode();
	    return hc;
	}

	public override bool Equals (object _p)
	{
		return this == (Point2)_p;
	}

    public override string ToString()
    {
        return String.Format("({0},{1})", x, y);
    }

    public static implicit operator Vector2(Point2 p)
	{
		return new Vector2(p.x, p.y);
	}
}

