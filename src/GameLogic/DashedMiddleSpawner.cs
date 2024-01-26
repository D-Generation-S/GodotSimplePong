using Godot;
using System.Diagnostics;

public partial class DashedMiddleSpawner : Node2D
{
	[Export]
	private CanvasTexture textureToUse;

	[Export]
	private int length = 5;

	[Export]
	private int width = 5;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Rect2 screenArea = GetViewportRect();
		Position = new Vector2(screenArea.Size.X / 2, 0);
		int numberOfBars = (int)(screenArea.Size.Y / (length * 2));
		numberOfBars += 2;

		for (int i = 0; i < numberOfBars; i++)
		{
			var sprite2D = new Sprite2D();
			int yPos = length * i * 2;
			yPos += length / 2;
			sprite2D.Texture = textureToUse;
			sprite2D.Scale = new Vector2(width, length);
			sprite2D.Position = new Vector2(0, yPos);
			
			AddChild(sprite2D);
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
