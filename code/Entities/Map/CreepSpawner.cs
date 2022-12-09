using System.Linq;
using Sandbox;
using Editor;

namespace TowerWars;

[HammerEntity]
[ClassName( "towerwars_creepspawner" ), Title( "Creep Spawner" ), Category( "Tower Wars" )]
public class CreepSpawner : Entity
{
	[Property]
	public int Team { get; set; } = 0;

	private CreepObjective _objective;
	private TimeSince _sinceLastSpawned = 0;

	[Event.Tick.Server]
	public void Tick()
	{
		if ( !_objective.IsValid() )
		{
			_objective = All.OfType<CreepObjective>().FirstOrDefault( o => o.Team == Team );

			if ( !_objective.IsValid() )
			{
				Log.Error( $"no objective for team {Team}!" );
				return;
			}
		}

		if ( _sinceLastSpawned >= 1f )
		{
			_sinceLastSpawned = 0;

			var creep = new SimpleCreep { Position = Position };
			creep.SetTarget( _objective.Position );
		}
	}
}
