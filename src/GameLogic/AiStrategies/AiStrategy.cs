using Godot;

public partial class AiStrategy : Node
{
    public virtual int GetMoveDirection(Vector2 ballPosition, float aiYPosition, Rect2 viewportRect)
    {
        return 0;
    }
}