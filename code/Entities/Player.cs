using Sandbox;

namespace TowerWars;

public partial class Player : Entity
{
	private Vector3 _wishPosition;
	private Vector3 _position;

	public override void ClientSpawn()
	{
		base.ClientSpawn();

		_wishPosition = Position;
		_position = Position;
	}

	public override void PostCameraSetup( ref CameraSetup camSetup )
	{
		base.PostCameraSetup( ref camSetup );
		
		var hSpeed = Input.Down( InputButton.Run ) ? 2000 : 1000;
		var movement = new Vector3( Input.Forward, Input.Left ).Normal * hSpeed;
		movement.z = -Input.MouseWheel * 3000;

		_wishPosition += movement * Time.Delta;

		_position = Vector3.Lerp( _position, _wishPosition, 0.25f );

		camSetup.Position = _position;
		camSetup.Rotation = Rotation.From(65, 0, 0);
	}
}
