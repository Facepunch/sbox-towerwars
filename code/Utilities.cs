using System;

namespace TowerWars;

public static class Utilities
{
	public const float CellSize = 40;

	public static Vector3 SnapToCell( Vector3 position )
	{
		position.x = MathF.Floor( position.x / CellSize ) * CellSize;
		position.y = MathF.Floor( position.y / CellSize ) * CellSize;
		return position + new Vector3( CellSize / 2, CellSize / 2, 0 );
	}
}
