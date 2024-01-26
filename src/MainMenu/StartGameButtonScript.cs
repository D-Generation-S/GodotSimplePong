using Godot;
using System.Diagnostics;

public partial class StartGameButtonScript : AutoFocusableButton
{
	[Export]
	private PackedScene scene;

	[Export]
	private PackedScene player1;

	[Export]
	private PackedScene player2;

	[Export]
	private Node nodeToClose;

    public override void _Ready()
    {
        base._Ready();
    }

    public override void _Pressed()
    {
		var newScene = ResourceLoader.Load<PackedScene>(scene.ResourcePath).Instantiate();
		var players = newScene.GetNode("./Players");
		foreach	(var child in players.GetChildren())
		{
			players.RemoveChild(child);
		}
		players.AddChild(player1.Instantiate());
		players.AddChild(player2.Instantiate());

		GetTree().Root.AddChild(newScene);
		GetTree().Root.RemoveChild(nodeToClose);
		QueueFree();
    }
}
