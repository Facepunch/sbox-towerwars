using System;
using Sandbox;

namespace TowerWars;

public class Projectile : ModelEntity
{
	public BaseCreep Target { get; init; }
	public float Duration { get; init; }

	private Vector3 _startPosition;
	private TimeSince _sinceCreated;

	public override void Spawn()
	{
		base.Spawn();
		
		SetModel( "models/projectile.vmdl" );
	}

	public void Start()
	{
		_startPosition = Position;
		_sinceCreated = 0;
	}

	[Event.Tick.Client]
	public void Tick()
	{
		if ( !Target.IsValid() || _sinceCreated >= Duration )
		{
			Delete();
			return;
		}

		// todo: shorten duration if the target moved closer?
		Position = Vector3.Lerp( _startPosition, Target.Position, Math.Clamp( _sinceCreated / Duration, 0, 1 ) );
	}
}
