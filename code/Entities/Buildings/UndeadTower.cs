using TowerWars;

public partial class UndeadTower : BaseProjectileTower
{
	public UndeadTower()
	{
		AttackInterval = 0.25f;
		Damage = 5f;
		Range = 250f;
	}

	public override void Spawn()
	{
		SetModel("models/undead_tower.vmdl");

		base.Spawn();
	}
}
