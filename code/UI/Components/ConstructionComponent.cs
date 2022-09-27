using System;
using System.Linq;
using Sandbox;
using Sandbox.UI;

namespace TowerWars;

[UseTemplate]
public class ConstructionComponent : HudComponent
{
	public TowerType? TowerType { get; private set; }

	private void Refresh()
	{
		var currentType = TowerType?.ToString();

		foreach ( var child in Children.OfType<Button>() )
		{
			var childType = child.GetAttribute( "type" );
			if ( childType != null )
			{
				child.SetClass( "selected", string.Equals( childType, currentType ) );
			}
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

	public override void OnSelect()
	{
		base.OnSelect();

		TowerType = null;
		Refresh();
	}

	public override void OnLeftClick( Vector3 position, BaseBuilding entity )
	{
		if ( TowerType == null || entity != null )
		{
			base.OnLeftClick( position, entity );
			return;
		}

		Commands.SpawnTower( TowerType.Value, position );
		Hud.Select( null );
		Refresh();
	}
}
