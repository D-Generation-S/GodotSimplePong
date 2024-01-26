using Godot;
using System;

public partial class MusicPlayer : AudioStreamPlayer
{
	[Export]
	private AudioStreamMP3[] files;

	private int currentIndex;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		currentIndex = 0;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (!Playing)
		{
			Stream = files[currentIndex];
			currentIndex++;
			currentIndex = currentIndex > files.Length ? 0 : currentIndex;
			Play();
		}
	}
}
