using Godot;

public partial class OpenScene : AutoFocusableButton
{
	[Export(PropertyHint.File, "*.tscn")]
	public string scenePath { get; set; }

	[Export]
	private Node nodeToClose;

    public override void _Ready()
    {
        base._Ready();
    }

	public void TriggerPressed()
	{
		_Pressed();
	}

    public override void _Pressed()
    {
		var newScene = ResourceLoader.Load<PackedScene>(scenePath).Instantiate();

		GetTree().Root.AddChild(newScene);
		if (nodeToClose is not null)
		{
			GetTree().Root.RemoveChild(nodeToClose);
		}
    }
}
