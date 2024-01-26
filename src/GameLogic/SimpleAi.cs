using System;
using System.Linq;
using Godot;

public partial class SimpleAi : MovementScript
{
	[Export]
	private NodePath strategyPath;

	private AiStrategy realStrategy;

	private Node2D ball;

	private float lastBallXPos;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ball = GetTree().Root.GetNode<Node2D>("./GameArea/Ball");
		realStrategy = GetChildren().OfType<AiStrategy>().FirstOrDefault();
		base._Ready();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		var realSpeed = GetRealSpeed(delta);
		var direction = realStrategy?.GetMoveDirection(ball.Position, Position.Y, GetViewportRect()) ?? 0;
		direction = Math.Min(Math.Max(-1, direction), 1);
		Position = new Vector2(Position.X, Position.Y + realSpeed * direction);
		lastBallXPos = ball.Position.Y;
		base._Process(delta);
	}
}

