using System.Linq;
using Sandbox;

namespace TowerWars;

public abstract partial class BaseTower : BaseBuilding
{
	public float AttackInterval { get; init; }

	private TimeSince _sinceLastAttack;

	public override void Spawn()
	{
		base.Spawn();

		_sinceLastAttack = Rand.Float( 0, AttackInterval );
	}

	[Event.Tick.Server]
	public void Tick()
	{
		if ( _sinceLastAttack >= AttackInterval )
		{
			_sinceLastAttack = 0;
			Attack();
		}
	}

	protected abstract void Attack();
}
