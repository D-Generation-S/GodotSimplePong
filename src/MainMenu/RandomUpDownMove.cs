using Godot;
using System;

public partial class RandomUpDownMove : MovementScript
{

	[Export]
	private Timer timer;

	[Export]
	private int moveSpeed = 5;
	
	[Export(PropertyHint.Range, "0.1, 1")]
	private float timerWaitTime = 1;

	private int currentDirection = 0;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		if (timer is not null)
		{
			timer.Timeout += () => {
				currentDirection = GetRandomDirection();
				timer.WaitTime = timerWaitTime;
				timer.Start();
			};
		}

		currentDirection = GetRandomDirection();
		timer.WaitTime = timerWaitTime;
		timer.Start();
		base._Ready();
	}

	private int GetRandomDirection()
	{
		return Random.Shared.NextDouble() < 0.5 ? -1 : 1;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		float realSpeed = GetRealSpeed(moveSpeed);
		Position = new Vector2(Position.X, Position.Y + realSpeed * currentDirection);
		base._Process(delta);
	}
}
