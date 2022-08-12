using TowerWars;

public partial class OrcTower : BaseProjectileTower
{
	public OrcTower()
	{
		AttackInterval = 0.75f;
		Damage = 25f;
		Range = 150f;
	}

	public override void Spawn()
	{
		SetModel("models/orc_tower.vmdl");

		base.Spawn();
	}
}
