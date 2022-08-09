using System;

namespace TowerWars;

public static class Utilities
{
	public const float CellSize = 40;

	public static Vector3 SnapForSpawning( Vector3 position )
	{
		position.x = MathF.Round( position.x / CellSize ) * CellSize;
		position.y = MathF.Round( position.y / CellSize ) * CellSize;
		return position;
	}
}
