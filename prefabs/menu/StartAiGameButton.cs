using System.Linq;
using Godot;

public partial class StartAiGameButton : Node
{
	
	[Export]
	private PackedScene gameScene;

	[Export]
	private PackedScene player1;

	
	[Export]
	private PackedScene aiPlayerTemplate;

	private Node nodeToClose;

	private AiSelectionButton parent;

    public override void _Ready()
    {
        nodeToClose = GetTree().Root.GetChildren().LastOrDefault();
		var name = nodeToClose.Name;
		parent = GetParent<AiSelectionButton>();
    }

    public void StartGame()
	{
		var newScene = gameScene.Instantiate();
		var players = newScene.GetNode("./Players");
		foreach	(var child in players.GetChildren())
		{
			players.RemoveChild(child);
		}
		players.AddChild(player1.Instantiate());
		var aiPlayer = aiPlayerTemplate.Instantiate();
		aiPlayer.AddChild(parent.GetAiPlayer().Instantiate());
		players.AddChild(aiPlayer);


		GetTree().Root.CallDeferred("add_child", newScene);
		GetTree().Root.CallDeferred("remove_child", nodeToClose);
	}
}
