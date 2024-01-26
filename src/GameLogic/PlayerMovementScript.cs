using Godot;
using System;
using System.Diagnostics;

public partial class PlayerMovementScript : MovementScript
{
	[Export]
	private string upName;

	[Export]
	private string downName;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		var realSpeed = GetRealSpeed(delta);

		if (Input.IsActionPressed(upName))
		{
			float newYPosition = Position.Y - Input.GetActionStrength(upName) * realSpeed;
			Position = new Vector2(Position.X, newYPosition);
			base._Process(delta);
			return;
		}

		if (Input.IsActionPressed(downName))
		{
			float newYPosition = Position.Y + Input.GetActionStrength(downName) * realSpeed;
			Position = new Vector2(Position.X, newYPosition);
			base._Process(delta);
			return;
		}
	}
}
