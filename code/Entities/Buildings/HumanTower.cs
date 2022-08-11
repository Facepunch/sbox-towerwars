using Sandbox;
using TowerWars;

public partial class HumanTower : BaseProjectileTower
{
	public HumanTower()
	{
		AttackInterval = 0.5f;
		Damage = 10f;
		Range = 200f;
	}

	public override void Spawn()
	{
		base.Spawn();
		
		SetModel("models/human_tower.vmdl");
		
		EnableAllCollisions = true;
		SetupPhysicsFromModel( PhysicsMotionType.Static );
	}
}
