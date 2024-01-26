using System;
using System.Linq;
using Godot;

public partial class AddBackgroundSceneComponent : Node
{
    [Export]
    private PackedScene backgroundScene;

    private Node instantiatedBackgroundScene;

    public override void _Ready()
    {
        if (backgroundScene is null)
        {
            return;
        }



        instantiatedBackgroundScene = backgroundScene.Instantiate();
        CallDeferred("add_child", instantiatedBackgroundScene);
    }

    

    public void RemoveScene()
    {
        if (instantiatedBackgroundScene is null)
        {
            return;
        }
        instantiatedBackgroundScene.QueueFree();
    }
}
