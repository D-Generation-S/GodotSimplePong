using Godot;
using System;
using System.Diagnostics;
using System.Linq;

public partial class GoalScored : Node2D
{
	[Signal]
	public delegate void UpdatePointsEventHandler();

	private Area2D localArea;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		localArea = GetChildren().OfType<Area2D>().FirstOrDefault();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void PlayerScoredGoal(Area2D area)
	{
		if (area == localArea)
		{
			EmitSignal(SignalName.UpdatePoints);
		}
	}
}
