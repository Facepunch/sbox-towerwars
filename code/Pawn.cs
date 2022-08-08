using Sandbox;

namespace TowerWars;

public partial class Pawn : Entity
{
	private Vector3 _wishPosition;

	public override void Spawn()
	{
		base.Spawn();

		_wishPosition = Position;
	}

	/// <summary>
	/// Called every tick, clientside and serverside.
	/// </summary>
	public override void Simulate( Client cl )
	{
		base.Simulate( cl );

		var hSpeed = Input.Down( InputButton.Run ) ? 2000 : 1000;
		var movement = new Vector3( Input.Forward, Input.Left ).Normal * hSpeed;
		movement.z = -Input.MouseWheel * 2000;

		_wishPosition += movement * Time.Delta;

		Position = Vector3.Lerp( Position, _wishPosition, 0.1f );
	}

	/// <summary>
	/// Called every frame on the client
	/// </summary>
	public override void FrameSimulate( Client cl )
	{
		base.FrameSimulate( cl );
		
		Rotation = Rotation.From(65, 0, 0);
		EyeRotation = Rotation;
	}
}
