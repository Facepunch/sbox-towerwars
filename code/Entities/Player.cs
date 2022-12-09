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


	[Event.Client.Frame]
	public void PostCameraSetup()
	{
		var hSpeed = Input.Down( InputButton.Run ) ? 2000 : 1000;
		var movement = new Vector3( Input.AnalogMove.x, Input.AnalogMove.y ).Normal * hSpeed;
		movement.z = -Input.MouseWheel * 3000;

		_wishPosition += movement * Time.Delta;

		_position = Vector3.Lerp( _position, _wishPosition, 0.25f );

		Camera.Position = _position;
		Camera.Rotation = Rotation.From(65, 0, 0);
	}
}
