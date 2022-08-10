using Sandbox;
using Sandbox.UI;
using TowerWars.Pathfinding;

namespace TowerWars;

public class MouseControl : Panel
{
	private int _mouseWheel;

	public override void OnMouseWheel( float value )
	{
		_mouseWheel = (int)(value * -1.0f);
	}

	[Event.BuildInput]
	public virtual void ProcessClientInput( InputBuilder input )
	{
		input.MouseWheel = _mouseWheel;
		_mouseWheel = 0;
	}

	public override void OnButtonEvent( ButtonEvent e )
	{
		if ( e.Pressed )
		{
			if ( e.Button == "mouseright" && TryPickPosition( out var movePos ) )
			{
				Commands.CreepMoveTest( movePos );
				return;
			}

			if ( e.Button == "mouseleft" && TryPickPosition( out var towerPos ) )
			{
				Commands.SpawnTower( towerPos );
				return;
			}

			if ( e.Button == "mousemiddle" && TryPickPosition( out var creepPos ) )
			{
				Commands.SpawnCreepTest( creepPos );
				return;
			}
		}

		base.OnButtonEvent( e );
	}

	private static bool TryPickPosition( out Vector3 position, Entity ignoreEntity = null )
	{
		var result = TraceFromCursor().Ignore( ignoreEntity ).Run();
		if ( result.Hit )
		{
			position = result.EndPosition;
			return true;
		}

		position = Vector3.Zero;
		return false;
	}

	private static Trace TraceFromCursor()
	{
		var ray = new Ray( CurrentView.Position, Screen.GetDirection( Mouse.Position ) );
		return Trace.Ray( ray.Origin, ray.Origin + ray.Direction * 2500 );
	}
}
