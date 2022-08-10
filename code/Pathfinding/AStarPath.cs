using System;
using System.Collections.Generic;

namespace TowerWars.Pathfinding;

public readonly struct AStarPath
{
	public ulong Version { get; }
	public List<Position> Path { get; }

	public AStarPath( ulong version, List<Position> path )
	{
		Version = version;
		Path = path ?? throw new ArgumentNullException( nameof(path) );
	}
}
