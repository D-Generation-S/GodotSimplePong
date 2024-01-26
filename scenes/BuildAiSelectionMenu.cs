using Godot;
using System;
using System.Diagnostics;
using System.Linq;

public partial class BuildAiSelectionMenu : VBoxContainer
{
	[Export]
	private AiDescriptionResource[] aiDescriptions;

	[Export(PropertyHint.File, "*.tscn")]
	public string buttonTemplatePath { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		if (buttonTemplatePath is null)
		{
			return;
		}
		bool isFirst = true;
		foreach(var description in aiDescriptions.OfType<AiDescriptionResource>())
		{
			var entity =  ResourceLoader.Load<PackedScene>(buttonTemplatePath).Instantiate<AiSelectionButton>();
			entity.Setup(description);
			if (isFirst)
			{
				entity.MakeAutoSelected();
				isFirst = false;
			}
			CallDeferred("add_child", entity);
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
