using Sandbox;
using TowerWars;

public partial class SimpleTower : BaseBuilding
{
	public override void Spawn()
	{
		base.Spawn();
		
		SetModel("models/simple_tower.vmdl");
		
		EnableAllCollisions = true;
		SetupPhysicsFromModel( PhysicsMotionType.Static );
	}
}
