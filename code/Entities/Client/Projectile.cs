using System;
using Sandbox;

namespace TowerWars;

public class Projectile : ModelEntity
{
	public Vector3 StartPosition { get; init; }
	public BaseCreep Target { get; init; }
	public float Duration { get; init; }

	private TimeSince _sinceCreated;

	public override void Spawn()
	{
		base.Spawn();

		_sinceCreated = 0;
		
		SetModel( "models/projectile.vmdl" );
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
		Position = Vector3.Lerp( StartPosition, Target.Position, Math.Clamp( _sinceCreated / Duration, 0, 1 ) );
	}
}
