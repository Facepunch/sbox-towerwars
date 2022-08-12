using Sandbox;
using SandboxEditor;

namespace TowerWars;

[HammerEntity]
[ClassName( "towerwars_creepobjective" ), Title( "Creep Objective" ), Category( "Tower Wars" )]
public class CreepObjective : Entity
{
	[Property]
	public int Team { get; set; } = 0;
}
