using Sandbox;

namespace TowerWars;

public partial class SimpleCreep : BaseCreep
{
	public override void Spawn()
	{
		base.Spawn();

		SetModel( "models/simple_creep.vmdl" );
	}
}
