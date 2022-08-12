using TowerWars;

public partial class ElfTower : BaseProjectileTower
{
	public ElfTower()
	{
		AttackInterval = 1f;
		Damage = 20f;
		Range = 300f;
	}

	public override void Spawn()
	{
		SetModel("models/elf_tower.vmdl");

		base.Spawn();
	}
}
