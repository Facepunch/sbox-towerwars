using Sandbox;
using Sandbox.UI;

namespace TowerWars;

public class MouseControl : HudComponent
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
			if ( e.Button == "mouseright" && TryPickPosition( out var movePos, out _ ) )
			{
				Commands.CreepMoveTest( movePos );
				return;
			}

			if ( e.Button == "mouseleft" && TryPickPosition( out var leftPos, out var leftEntity ) )
			{
				Hud.Selected?.OnLeftClick( leftPos, leftEntity );
				return;
			}

			if ( e.Button == "mousemiddle" && TryPickPosition( out var creepPos, out _ ) )
			{
				Commands.SpawnCreepTest( creepPos );
				return;
			}
		}

		base.OnButtonEvent( e );
	}

	private static bool TryPickPosition( out Vector3 position, out Entity entity )
	{
		var result = TraceFromCursor().Run();
		if ( result.Hit )
		{
			position = result.EndPosition;
			entity = result.Entity;
			return true;
		}

		position = Vector3.Zero;
		entity = null;
		return false;
	}

	private static Trace TraceFromCursor()
	{
		var ray = new Ray( CurrentView.Position, Screen.GetDirection( Mouse.Position ) );
		return Trace.Ray( ray.Origin, ray.Origin + ray.Direction * 2500 );
	}
}
