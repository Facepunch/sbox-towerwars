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

		if ( World.ServerInstance.TryPlace( position ) )
		{
			var tower = ConstructTower( type );
			tower.Position = position;
		}
		else
		{
			Log.Warning( "occupied" );
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

	[ConCmd.Server]
	public static void SpawnCreepTest( Vector3 position )
	{
		var creep = new SimpleCreep();
		creep.Position = position;
	}

	[ConCmd.Server]
	public static void CreepMoveTest( Vector3 position )
	{
		foreach ( var creep in Entity.All.OfType<BaseCreep>() )
		{
			creep.SetTarget( position );
		}
	}
}
