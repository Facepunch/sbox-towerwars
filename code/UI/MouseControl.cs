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
		if ( e.Pressed && e.Button == "mouseright" )
		{
			Log.Info(Map.Physics.Body.GetBounds());
			var world = new AStarWorld( 10, 10 );
			world[new Position( 1, 0 )] = true;

			var snapshot = world.GetSnapshot();
			var path = snapshot.FindPath( new Position( 0, 0 ), new Position( 2, 0 ) );
			if ( path != null )
			{
				Log.Info( string.Join( ", ", path.Value.Path ) );
			}
			else
			{
				Log.Info( "no path" );
			}

			return;
		}

		if ( e.Pressed && e.Button == "mouseleft" && TryPickPosition( out var position ) )
		{
			Log.Info( e.Button );

			Commands.SpawnTest( position );
			return;
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
