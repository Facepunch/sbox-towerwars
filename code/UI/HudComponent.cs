using System;
using Sandbox.UI;

namespace TowerWars;

public class HudComponent : Panel
{
	public Hud Hud { get; private set; }
	public bool IsSelected => ReferenceEquals( Hud?.Selected, this );

	public virtual void Initialize( Hud hud )
	{
		Hud = hud ?? throw new ArgumentNullException( nameof( hud ) );
	}

	public virtual void OnSelect()
	{
		SetClass( "selected", true );
	}

	public virtual void OnDeselect()
	{
		SetClass( "selected", false );
	}

	public virtual void OnLeftClick( Vector3 position, BaseBuilding entity )
	{
		if ( entity == null )
		{
			Hud.Select( null );
		}
		else if ( entity is BaseTower tower )
		{
			Hud.InspectTower.Tower = tower;
			Hud.Select( Hud.InspectTower );
		}
	}

	public void Deselect() => Hud?.Select( null );
}
