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

	[Event.Client.BuildInput]
	public virtual void ProcessClientInput()
	{
		Input.MouseWheel = _mouseWheel;
		_mouseWheel = 0;
	}

	public override void OnButtonEvent( ButtonEvent e )
	{
		if ( e.Pressed )
		{
			if ( e.Button == "mouseleft" && TryPickPosition( out var leftPos, out var leftEntity ) )
			{
				Hud.Selected?.OnLeftClick( leftPos, leftEntity );
				return;
			}
		}

		base.OnButtonEvent( e );
	}

	private static bool TryPickPosition( out Vector3 position, out BaseBuilding entity )
	{
		var result = TraceFromCursor().Run();
		if ( result.Hit )
		{
			position = result.EndPosition;
			entity = result.Entity as BaseBuilding;
			return true;
		}

		position = Vector3.Zero;
		entity = null;
		return false;
	}

	private static Trace TraceFromCursor()
	{
		var ray = new Ray( Camera.Position, Screen.GetDirection( Mouse.Position ) );
		return Trace.Ray( ray.Position, ray.Position + ray.Forward * 2500 );
	}
}
