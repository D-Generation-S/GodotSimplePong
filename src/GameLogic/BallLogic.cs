using Godot;
using System;
using System.Diagnostics;
using System.Linq;

public partial class BallLogic : Node2D
{

	[Signal]
	public delegate void TimerUpdatedEventHandler(double timeLeft);

	
	[Signal]
	public delegate void TimerStartedEventHandler(int initial);

	[Export]
	private CollisionShape2D ballCollider;

	[Export]
	private Vector2 velocity;

	[Export]
	private int timeForTimer = 3;

	[Export]
	private int speed;

	[Export]
	private Timer timer;

	[Export]
	private float yDifferenceBaseValue = 0.3f;

	[Export]
	private AudioStreamPlayer2D effectPlayer;

	[Export]
	private AudioStreamMP3 wallHit;

	

	[Export(PropertyHint.File, "*.tscn")]
	private string ballLostTemplate;

	[Export]
	private AudioStreamMP3 ballLost;

	[Export]
	private AudioStreamMP3 ballStart;

	private bool player1Start = true;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		timer.Timeout += () => {
			float xVelocity = player1Start ?  -0.5f : 0.5f;
			float yVelocity = (float)Math.Max(Random.Shared.NextDouble(), 0.5);
			bool up = Random.Shared.NextDouble() < 0.5 ? true : false;
			velocity = new Vector2(xVelocity, yVelocity);
			PlaySound(ballStart);
		};
		ResetBall(false);
		var players = GetNode("/root/GameArea/Players");
		foreach (var player in players.GetChildren())
		{
			var playerCollider = player.GetChildren().OfType<Area2D>().FirstOrDefault();
			if (playerCollider is not null)
			{ 
				playerCollider.AreaEntered += ballCollider => {
					if(ballCollider != this.ballCollider.GetParent<Area2D>())
					{
						return;
					}
					var player = playerCollider.GetParent() as Node2D;
					velocity = new Vector2(velocity.X * -1, velocity.Y);
					float topTarget = player.Position.Y - player.Scale.Y / 2;
					float bottomTarget = player.Position.Y + player.Scale.Y / 2;
					PlaySound(wallHit);

					if ( topTarget < Position.Y && bottomTarget > Position.Y)
					{
						float baseLine = Math.Abs(player.Position.Y - Position.Y);
						float sideLength = player.Scale.Y / 2;
						float percentage = baseLine / sideLength;
						float difference = yDifferenceBaseValue * percentage;
						difference *= Position.Y < player.Position.Y ? -1 : 1;

						velocity = new Vector2(velocity.X, velocity.Y + difference).Normalized();
					}
				};
			}
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		var realVelocity = velocity.Normalized();
		realVelocity *= (float)(delta +1) * speed;

		EmitSignal(SignalName.TimerUpdated, timer.TimeLeft);

		Position += realVelocity;
	}

	public void ResetBall(bool doEffects)
	{

		ResetTimer();
		
		if (doEffects)
		{
			var emitter = ResourceLoader.Load<PackedScene>(ballLostTemplate)?.Instantiate() as Node2D;
			emitter.Position = Position;
			
        	GetTree().Root.GetNode("./GameArea").CallDeferred("add_child", emitter);
		}
		var area = GetViewportRect();
		Position = new Vector2(area.Size.X / 2, area.Size.Y / 2);
		velocity = Vector2.Zero;
		player1Start = !player1Start;
		
	}

	public void ResetBall()
	{
		ResetBall(true);
	}

	private void ResetTimer()
	{
		timer.WaitTime = timeForTimer;
		timer.Start();
		EmitSignal(SignalName.TimerStarted, timeForTimer);
	}

	private void OnWallHit(Area2D area)
	{
		if (area == ballCollider.GetParent<Area2D>())
		{
			velocity = new Vector2(velocity.X, velocity.Y * -1);
			PlaySound(wallHit);
		}
	}

	private void PlaySound(AudioStreamMP3 streamMp3)
	{
		if (effectPlayer is null || streamMp3 is null)
		{
			return;
		}
		effectPlayer.Stream = streamMp3;
		effectPlayer.Play();
	}
}
