using Godot;
using System;

public partial class ScoreScript : RichTextLabel
{
	private int currentScore = 0;

	private bool scoreChanged = false;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		scoreChanged = true;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (!scoreChanged)
		{
			return;
		}
		Text = currentScore.ToString();
		scoreChanged = false;
	}

	private void IncreaseScore()
	{
		currentScore++;
		scoreChanged = true;
	}
}
