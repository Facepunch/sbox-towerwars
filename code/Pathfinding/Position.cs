using System;

namespace TowerWars.Pathfinding;

public readonly struct Position : IEquatable<Position>
{
	public int X { get; }
	public int Y { get; }

	public Position( int x, int y )
	{
		X = x;
		Y = y;
	}

	public override string ToString()
	{
		return $"({X},{Y})";
	}

	public bool Equals( Position other )
	{
		return X == other.X && Y == other.Y;
	}

	public override bool Equals( object obj )
	{
		return obj is Position other && Equals( other );
	}

	public override int GetHashCode()
	{
		return HashCode.Combine( X, Y );
	}

	public static bool operator ==( Position left, Position right )
	{
		return left.Equals( right );
	}

	public static bool operator !=( Position left, Position right )
	{
		return !left.Equals( right );
	}

	public static float Distance( in Position a, in Position b )
	{
		return Vector3.DistanceBetween( new Vector3( a.X, a.Y ), new Vector3( b.X, b.Y ) );
	}
}
