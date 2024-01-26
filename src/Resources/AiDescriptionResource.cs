using Godot;

[GlobalClass]
public partial class AiDescriptionResource : Resource
{
    [Export]
    private string displayName;
    
    [Export(PropertyHint.File, "*.tscn")]
    private string aiNodePath;

    public string GetDisplayName()
    {
        return displayName;
    }

    public PackedScene GetAiNode()
    {
        return ResourceLoader.Load<PackedScene>(aiNodePath);
    }
}