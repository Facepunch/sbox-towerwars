using System.Linq;
using Sandbox;

namespace TowerWars;

public static partial class Commands
{
	[ConCmd.Server]
	public static void SpawnTower( Vector3 position )
	{
		position = Utilities.SnapToCell( position );

		if ( World.ServerInstance.TryPlace( position ) )
		{
			var tower = new SimpleTower();
			tower.Position = position;
		}
		else
		{
			Log.Warning( "occupied" );
		}
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
