using Sandbox;
using Editor;

namespace TowerWars;

[HammerEntity]
[ClassName( "towerwars_nobuild" ), Title( "No Build Zone" ), Category( "Tower Wars" )]
[BoundsHelper( "extents", true )]
public class NoBuildZone : Entity
{
	[Property, DefaultValue("100 100 100")]
	public Vector3 Extents { get; set; } = new Vector3( 100 );

	public BBox BoundingBox
	{
		get
		{
			var origin = Position;
			var halfExtents = Extents * 0.5f;
			return new BBox( origin - halfExtents, origin + halfExtents );
		}
	}
}
