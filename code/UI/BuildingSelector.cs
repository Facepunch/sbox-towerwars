using System;
using Sandbox;
using Sandbox.UI;

namespace TowerWars;

[UseTemplate]
public class BuildingSelector : HudComponent
{
	public TowerType TowerType { get; private set; } = TowerType.Human;

	public void Choose( string typeStr )
	{
		if ( Enum.TryParse<TowerType>( typeStr, out var type ) )
		{
			TowerType = type;
			Hud.Select( this );
			Log.Info( $"Selected {type}" );
		}
		else
		{
			Log.Warning( $"Unsupported tower type: {typeStr}" );
		}
	}

	public override void OnLeftClick( Vector3 position, Entity entity )
	{
		Commands.SpawnTower( TowerType, position );
	}
}
