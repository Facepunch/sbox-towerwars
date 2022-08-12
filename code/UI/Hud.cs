using Sandbox;
using Sandbox.UI;

namespace TowerWars;

public partial class Hud : HudEntity<RootPanel>
{
	public MouseControl MouseControl { get; }
	public BuildingSelector BuildingSelector { get; }

	public HudComponent Selected { get; private set; }

	public Hud()
	{
		if ( IsClient )
		{
			RootPanel.StyleSheet.Load( "/UI/Styles/tower_wars.scss" );

			MouseControl = AddComponent<MouseControl>();
			BuildingSelector = AddComponent<BuildingSelector>();

			T AddComponent<T>() where T : HudComponent, new()
			{
				var component = RootPanel.AddChild<T>();
				component.Initialize( this );
				return component;
			}
		}
	}

	public void Select( HudComponent component )
	{
		if ( Selected == component )
		{
			return;
		}

		Selected?.OnDeselect();
		Selected = component;
		Selected?.OnSelect();
	}
}
