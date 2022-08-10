using System.Collections.Generic;
using System.Linq;
using Sandbox;
using TowerWars.Pathfinding;

namespace TowerWars;

public class World : Entity
{
	public static World ServerInstance { get; private set; }

	private BBox _worldBounds;
	private AStarWorld _pathfindWorld;

	public override void Spawn()
	{
		base.Spawn();

		if ( IsServer )
		{
			if ( ServerInstance == null )
			{
				ServerInstance = this;

				Initialize();
			}
			else
			{
				Log.Error( "World entity spawned twice?!" );
			}
		}
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();

		if ( IsServer && ServerInstance == this )
		{
			ServerInstance = null;
		}
	}

	private void Initialize()
	{
		var mapBounds = Map.Physics.Body.GetBounds();
		var width = (int)((mapBounds.Size.x + Utilities.CellSize - 1) / Utilities.CellSize);
		var height = (int)((mapBounds.Size.y + Utilities.CellSize - 1) / Utilities.CellSize);

		_worldBounds = mapBounds;
		_pathfindWorld = new AStarWorld( width, height );
	}

	public bool TryPlace( Vector3 position )
	{
		var pathfindPos = WorldToPathfind( position );
		if ( _pathfindWorld[pathfindPos] )
		{
			return false; // occupied
		}

		_pathfindWorld[pathfindPos] = true;
		return true;
	}

	public bool TryFindPath( Vector3 from, Vector3 to, out List<Vector2> path )
	{
		var result = _pathfindWorld.GetSnapshot().FindPath( WorldToPathfind( from ), WorldToPathfind( to ) );

		if ( result == null )
		{
			path = null;
			return false;
		}

		path = result.Value.Path.Select( p => PathfindToWorld( p ) ).ToList();
		return true;
	}

	private Position WorldToPathfind( Vector3 position )
	{
		return new Position(
			(int)((position.x - _worldBounds.Mins.x) / Utilities.CellSize),
			(int)((position.y - _worldBounds.Mins.y) / Utilities.CellSize) );
	}

	private Vector2 PathfindToWorld( Position position )
	{
		return new Vector2(
			_worldBounds.Mins.x + position.X * Utilities.CellSize,
			_worldBounds.Mins.y + position.Y * Utilities.CellSize );
	}
}
