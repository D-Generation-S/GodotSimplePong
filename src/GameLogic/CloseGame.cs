using Godot;
using System;

public partial class CloseGame : Node
{	
	[Export(PropertyHint.File, "*.tscn")]
	public string scenePath { get; set; }

	[Export]
	private Node nodeToClose;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsActionPressed("ui_cancel"))
		{
			var newScene = ResourceLoader.Load<PackedScene>(scenePath).Instantiate();

			GetTree().Root.AddChild(newScene);
			if (nodeToClose is not null)
			{
				GetTree().Root.RemoveChild(nodeToClose);
			}
		}
	}
}
