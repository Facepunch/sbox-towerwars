using System;
using System.Collections.Generic;

namespace TowerWars.Pathfinding;

public readonly struct AStarPath
{
	public bool Found { get; }
	public List<Position> Path { get; }

	public AStarPath( List<Position> path )
	{
		Found = true;
		Path = path ?? throw new ArgumentNullException( nameof(path) );
	}
}
