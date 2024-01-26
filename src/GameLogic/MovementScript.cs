using Godot;
using System;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

public partial class MovementScript : Node2D
{
    [Export]
    protected int speed = 1;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var area = GetViewportRect();
		Position = new Vector2(Position.X, area.Size.Y / 2);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		CheckBorders();
	}

	protected float GetRealSpeed(double delta)
	{
		return speed * ((float)delta + 1);
	}

	protected void CheckBorders()
	{
		if (Position.Y - Scale.Y / 2 < 0)
		{
			Position = new Vector2(Position.X, Scale.Y / 2);
		}

		if (Position.Y + Scale.Y / 2 > GetViewportRect().Size.Y)
		{
			Position = new Vector2(Position.X, GetViewportRect().Size.Y - Scale.Y / 2);
		}
	}
}
