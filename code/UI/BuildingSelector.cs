using System;
using System.Linq;
using Sandbox;
using Sandbox.UI;

namespace TowerWars;

[UseTemplate]
public class BuildingSelector : HudComponent
{
	public TowerType TowerType { get; private set; } = TowerType.Human;

	private void Refresh()
	{
		var currentType = IsSelected ? TowerType.ToString() : "";

		foreach ( var child in Children.OfType<Button>() )
		{
			var childType = child.GetAttribute( "type" );
			child.SetClass( "selected", string.Equals( childType, currentType ) );
		}
	}

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

		Refresh();
	}

	public override void OnLeftClick( Vector3 position, Entity entity )
	{
		Commands.SpawnTower( TowerType, position );
		Hud.Select( null );
		Refresh();
	}
}
