namespace TowerWars;

public partial class SimpleCreep : BaseCreep
{
	public SimpleCreep()
	{
		MoveSpeed = 200f;
		Health = 100f;
	}

	public override void Spawn()
	{
		base.Spawn();
		
		SetModel( "models/simple_creep.vmdl" );
	}
}
