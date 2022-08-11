using Sandbox;

namespace TowerWars;

public abstract partial class BaseTower : BaseBuilding
{
	public float AttackInterval { get; init; }
	public float Damage { get; init; }

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

	public void HurtCreep( BaseCreep creep, float delay = 0f )
	{
		if ( !creep.IsValid() )
		{
			return;
		}

		if ( delay <= 0 )
		{
			creep.Hurt( Damage );
		}
		else
		{
			HurtCreepDelayed( creep, delay );
		}
	}

	private async void HurtCreepDelayed( BaseCreep creep, float delay )
	{
		await GameTask.DelaySeconds( delay );

		if ( creep.IsValid() )
		{
			creep.Hurt( Damage );
		}
	}
}
