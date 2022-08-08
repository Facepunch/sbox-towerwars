using Sandbox;
using Sandbox.UI;

namespace TowerWars;

public partial class Hud : HudEntity<RootPanel>
{
	public Hud()
	{
		if (IsClient)
		{
			RootPanel.StyleSheet.Load("/UI/Styles/tower_wars.scss");

			RootPanel.AddChild<MouseControl>();
		}
	}
}
