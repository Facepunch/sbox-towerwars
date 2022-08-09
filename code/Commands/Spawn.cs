using Sandbox;

namespace TowerWars;

public static partial class Commands
{
	[ConCmd.Server]
	public static void SpawnTest( Vector3 position )
	{
		var tower = new SimpleTower();
		tower.Position = Utilities.SnapForSpawning( position );
	}
}
