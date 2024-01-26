using Godot;
using System;

public partial class AutoFocusableButton : Button
{
	[Export]
	protected bool shouldGetInitialFocus = false;

	[Export]
	private AudioStreamMP3 selectedSound;

	private AudioStreamPlayer2D streamPlayer2D;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		if (shouldGetInitialFocus)
		{
			GrabFocus();
		}
		if (selectedSound is not null)
		{
            streamPlayer2D = new AudioStreamPlayer2D
            {
                Bus = "Effects",
				Stream = selectedSound
            };
            AddChild(streamPlayer2D);
			FocusEntered += GotFocus;
		}

	}

    private void GotFocus()
    {
        streamPlayer2D.Play();
    }
}
