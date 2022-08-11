using System;
using System.Collections.Generic;
using System.Linq;
using Sandbox;
using TowerWars.Pathfinding;

namespace TowerWars;

public class World : Entity
{
	public const float CellSize = 40;

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
		var width = (int)((mapBounds.Size.x + CellSize - 1) / CellSize);
		var height = (int)((mapBounds.Size.y + CellSize - 1) / CellSize);

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
		//DebugDrawWorld();

		var result = _pathfindWorld.GetSnapshot().FindPath( WorldToPathfind( from ), WorldToPathfind( to ) );

		if ( result == null )
		{
			path = null;
			return false;
		}

		path = result.Value.Path.Select( p => PathfindToWorld( p ) ).ToList();
		return true;
	}

	public Vector3 SnapToCell( Vector3 position )
	{
		return new Vector3( PathfindToWorld( WorldToPathfind( position ) ), 0 );
	}

	private Position WorldToPathfind( Vector3 position )
	{
		return new Position(
			(int)((position.x - _worldBounds.Mins.x) / CellSize),
			(int)((position.y - _worldBounds.Mins.y) / CellSize) );
	}

	private Vector2 PathfindToWorld( Position position )
	{
		return new Vector2(
			(_worldBounds.Mins.x + position.X * CellSize) + (CellSize / 2),
			(_worldBounds.Mins.y + position.Y * CellSize) + (CellSize / 2) );
	}

	private void DebugDrawWorld()
	{
		var rotation = Rotation.From( 90, 0, 0 );
		for ( var y = 0; y < _pathfindWorld.Height; y++ )
		{
			for ( var x = 0; x < _pathfindWorld.Width; x++ )
			{
				var pos = new Position( x, y );
				if ( !_pathfindWorld[pos] )
				{
					continue;
				}

				var color = _pathfindWorld[pos] ? Color.Red : Color.Black;
				DebugOverlay.Circle( PathfindToWorld( pos ), rotation, CellSize / 2, color, 5, false );
			}
		}
	}
}
