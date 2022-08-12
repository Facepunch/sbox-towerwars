using System;
using Sandbox;
using Sandbox.UI;

namespace TowerWars;

public class HudComponent : Panel
{
	public Hud Hud { get; private set; }

	public virtual void Initialize(Hud hud)
	{
		Hud = hud ?? throw new ArgumentNullException( nameof(hud) );
	}

	public virtual void OnSelect()
	{

	}

	public virtual void OnDeselect()
	{

	}

	public virtual void OnLeftClick( Vector3 position, Entity entity )
	{

	}
}
