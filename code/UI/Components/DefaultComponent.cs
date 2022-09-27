using Sandbox.UI;

namespace TowerWars;

[UseTemplate]
public class DefaultComponent : HudComponent
{
	public void Build() => Hud.Select( Hud.Construction );
}
