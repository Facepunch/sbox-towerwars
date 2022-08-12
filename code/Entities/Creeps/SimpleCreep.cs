using Sandbox;

namespace TowerWars;

public partial class SimpleCreep : BaseCreep
{
	public SimpleCreep()
	{
		MoveSpeed = Rand.Float(200, 500);
		Health = Rand.Float( 100, 400 );
	}

	public override void Spawn()
	{
		base.Spawn();
		
		SetModel( "models/simple_creep.vmdl" );
	}
}
