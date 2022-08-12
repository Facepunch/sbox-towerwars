using System.Collections.Generic;
using Sandbox;

namespace TowerWars;

public abstract partial class BaseCreep : AnimatedEntity
{
	public float MoveSpeed { get; init; }

	private Vector3? _targetPos;
	private List<Vector2> _waypoints;

	public override void Spawn()
	{
		base.Spawn();

		EnableAllCollisions = true;
		SetupPhysicsFromModel( PhysicsMotionType.Keyframed );
	}

	public void SetTarget( Vector3 position )
	{
		_targetPos = position.WithZ( Position.z );
		_waypoints = null;
	}

	[Event.Tick.Server]
	public void Tick()
	{
		if ( _targetPos == null )
		{
			_targetPos = null;
			_waypoints = null;
			return;
		}

		if ( _waypoints == null ) // todo: path invalidation
		{
			if ( World.ServerInstance.TryFindPath( Position, _targetPos.Value, out var path ) )
			{
				_waypoints = path;
			}
			else
			{
				Log.Warning( "no path to target!" );
				Delete();
				return;
			}
		}

		var moveDistance = MoveSpeed * Time.Delta;
		var position = (Vector2)Position;

		while ( moveDistance > 0 && _waypoints.Count > 0 )
		{
			var distance = Vector2.Distance( position, _waypoints[0] );

			if ( moveDistance >= distance )
			{
				moveDistance -= distance;
				position = _waypoints[0];
				_waypoints.RemoveAt( 0 );
			}
			else
			{
				position = Vector2.Lerp( position, _waypoints[0], moveDistance / distance );
				break;
			}
		}

		Position = new Vector3( position, Position.z ); // todo: raycast down to find correct Z

		if ( _waypoints.Count == 0 )
		{
			Log.Info( "hit objective" );
			Delete();
		}
	}

	public void Hurt( float damage )
	{
		TakeDamage( new DamageInfo { Damage = damage } );
	}
}
