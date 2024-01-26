using Godot;
using System;

public partial class TimerScript : RichTextLabel
{
	[Export]
	private Color color;

	[Export]
	private AudioStreamPlayer2D audioPlayer;

	private int lastNumberPlayed = 0;


    public override void _Ready()
    {
        BbcodeEnabled = true;
		Modulate = color;
		
    }

    private void TimerStarted(int initial)
	{
		Visible = true;
		Text = BuildText(initial);
		lastNumberPlayed = initial;
	}

	private void TimerStopped()
	{
		Visible = false;
	}

	private void TimerChanged(double time)
	{
		var currentTime = (int)Math.Round(time);
		Text = BuildText(currentTime);
		if (currentTime != lastNumberPlayed)
		{
			audioPlayer?.Play();
			lastNumberPlayed = currentTime;
		}
	}

	private string BuildText(int number)
	{
		return $"[center]{number}[/center]";
	}
}
