using Sandbox;
using TowerWars;

public partial class SimpleTower : BaseProjectileTower
{
	public SimpleTower()
	{
		AttackInterval = 0.5f;
		Range = 200f;
	}

	public override void Spawn()
	{
		base.Spawn();
		
		SetModel("models/simple_tower.vmdl");
		
		EnableAllCollisions = true;
		SetupPhysicsFromModel( PhysicsMotionType.Static );
	}
}
