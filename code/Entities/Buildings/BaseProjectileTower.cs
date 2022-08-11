using System.Linq;
using Sandbox;

namespace TowerWars;

public abstract partial class BaseProjectileTower : BaseTower
{
	public float Range { get; init; }

	protected override void Attack()
	{
		var origin = Position;
		var closestCreep = FindInSphere( Position, Range )
			.OfType<BaseCreep>()
			.MinBy( c => Vector3.DistanceBetween( origin, c.Position ) );

		if ( closestCreep.IsValid() )
		{
			var distance = Vector3.DistanceBetween( Position, closestCreep.Position );
			var duration = distance / 250f;

			ClientProjectile( closestCreep, duration );
			HurtCreep( closestCreep, duration );
		}
	}

	[ClientRpc]
	public void ClientProjectile( BaseCreep creep, float duration )
	{
		if ( creep.IsValid() )
		{
			var origin = GetAttachment( "muzzle" )?.Position ?? Position;

			new Projectile
			{
				Position = origin,
				Target = creep,
				Duration = duration,
			}.Start();
		}
	}
}
