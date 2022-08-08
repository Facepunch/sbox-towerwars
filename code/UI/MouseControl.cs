using Sandbox;
using Sandbox.UI;

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
}
