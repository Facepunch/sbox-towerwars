using Sandbox.UI;

namespace TowerWars;

[UseTemplate]
public class InspectTowerComponent : HudComponent
{
	public BaseTower Tower { get; set; }

	public string Type => Tower?.GetType()?.Name;
	public string Damage => Tower?.Damage.ToString();
	public string Speed => Tower?.AttackInterval.ToString();
}
