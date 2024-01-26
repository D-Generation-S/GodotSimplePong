using System;
using Godot;

public partial class ChaseStrategy : AiStrategy
{
    [Export]
    private int maxBallJumpRange = 100;

	private float lastBallXPos;

    public override int GetMoveDirection(Vector2 ballPosition, float aiYPosition, Rect2 viewportRect)
    {
        bool ballClosingIn = lastBallXPos < ballPosition.X && Math.Abs(lastBallXPos - ballPosition.X) < maxBallJumpRange;
		float target = ballClosingIn ? ballPosition.Y : viewportRect.Size.Y / 2;

        lastBallXPos = ballPosition.X;
        if (target == aiYPosition)
        {
            return 0;
        }
        return aiYPosition < target ? 1 : -1;
    }
}