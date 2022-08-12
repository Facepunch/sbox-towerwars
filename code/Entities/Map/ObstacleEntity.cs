using System.Collections.Generic;
using Sandbox;
using SandboxEditor;
using TowerWars.Pathfinding;

namespace TowerWars;

[HammerEntity]
[ClassName( "towerwars_obstacle" ), Title( "Obstacle" ), Category( "Tower Wars" )]
[BoundsHelper( "extents", true )]
public class ObstacleEntity : Entity
{
	[Property, DefaultValue("100 100 100")]
	public Vector3 Extents { get; set; } = new Vector3( 100 );

	public IEnumerable<Position> EnumerateCells( World world )
	{
		var origin = Position.WithZ( 0 );
		var halfExtents = Extents * 0.5f;

		for ( var y = origin.y - halfExtents.y; y <= origin.y + halfExtents.y; y += World.CellSize )
		{
			for ( var x = origin.x - halfExtents.x; x <= origin.x + halfExtents.x; x += World.CellSize )
			{
				yield return world.WorldToPathfind( new Vector3( x, y ) );
			}
		}
	}
}
