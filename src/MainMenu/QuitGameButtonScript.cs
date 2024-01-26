using Godot;
using System;

public partial class QuitGameButtonScript : AutoFocusableButton
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        base._Ready();
	}

    public override void _Pressed()
    {
		GetTree().Quit();
    }
}
