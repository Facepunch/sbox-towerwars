using System;
using System.Linq;
using Sandbox;

namespace TowerWars;

public enum TowerType
{
	Human, Orc, Undead, Elf,
}

public static partial class Commands
{
	[ConCmd.Server]
	public static void SpawnTower( TowerType type, Vector3 position )
	{
		position = World.ServerInstance.SnapToCell( position );

		var spawner = Entity.All.OfType<CreepSpawner>().Single();
		var objective = Entity.All.OfType<CreepObjective>().Single();

		if ( World.ServerInstance.TryPlace( position, spawner.Position, objective.Position ) )
		{
			var tower = ConstructTower( type );
			tower.Position = position;
		}

		static BaseTower ConstructTower( TowerType type ) => type switch
		{
			TowerType.Human => new HumanTower(),
			TowerType.Orc => new OrcTower(),
			TowerType.Undead => new UndeadTower(),
			TowerType.Elf => new ElfTower(),
			_ => throw new NotSupportedException( $"{type}" ),
		};
	}
}
