using Godot;
using System;
using System.Linq;

public partial class AiSelectionButton : AutoFocusableButton
{
	private AiDescriptionResource resource;	

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
	}

	public void Setup(AiDescriptionResource resource)
	{
		if (resource is null)
		{
			return;
		}

		this.resource = resource;
		Name = resource.GetDisplayName();
		Text = resource.GetDisplayName();
	}

	public PackedScene GetAiPlayer()
	{
		return resource.GetAiNode();
	}

	public void MakeAutoSelected()
	{
		HasFocus();
		shouldGetInitialFocus = true;
	}

    public override void _Pressed()
    {
        base._Pressed();
    }
}
