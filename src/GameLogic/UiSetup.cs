using Godot;

public partial class UiSetup : CanvasGroup
{

	[Export]
	private RichTextLabel player1;

	[Export]
	private RichTextLabel player1ScoreCount;

	[Export]
	private RichTextLabel player2;

	[Export]
	private RichTextLabel player2ScoreCount;


	[Export]
	private RichTextLabel timer;

	[Export]
	private int scoreHeight = 150;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var viewportArea =  GetViewportRect();
		var areaPart = viewportArea.Size.X / 4;

		player1.Position = new Vector2(areaPart, scoreHeight);
		player1.Size = new Vector2(areaPart, player1.Size.Y);
		FixPlayerTextIfNeeded(player1);
		SetScorePosition(player1ScoreCount, player1);
		
		player2.Position = new Vector2(areaPart * 2, scoreHeight);
		player2.Size = new Vector2(areaPart, player1.Size.Y);
		FixPlayerTextIfNeeded(player2);
		SetScorePosition(player2ScoreCount, player2);

		timer.Position = new Vector2(viewportArea.Size.X / 2 - timer.Size.X / 2, viewportArea.Size.Y / 2 - scoreHeight);
		timer.Text = "T";
	}

	private void SetScorePosition(RichTextLabel score, RichTextLabel player)
	{
		score.Position = new Vector2(player.Position.X + player.Size.X / 2, player.Position.Y + player.Size.Y);
	}

	private void FixPlayerTextIfNeeded(RichTextLabel player)
	{
		player.BbcodeEnabled = true;
		if (player.Text.StartsWith("[center]"))
		{
			return;
		}

		player.Text = $"[center]{player.Text}[/center]";
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
