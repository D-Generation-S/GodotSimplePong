using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Godot;

public partial class PredictStrategy : AiStrategy
{
    Vector2 lastBallPosition = Vector2.Zero;

    private AiStrategy subStrategy;

    [Export]
    private int predictions = 3;

    [Export]
    private int additionalBorderPixels = 0;

    
    [Export]
    private int screenSplitAmount = 6;

    [ExportGroup("Debug")]
    [Export]
    private bool isDebug = false;

    
    [ExportGroup("Debug")]
    [Export]
    private PackedScene debugEntity;

    private List<Node2D> debugEntities;

    public PredictStrategy()
    {
        subStrategy = new ChaseStrategy();
    }

    public override void _Ready()
    {
        if (debugEntity is not null && isDebug)
        {
            debugEntities = new();
            for (int i = 0; i < predictions + 1; i++)
            {
                var entity = debugEntity.Instantiate<Node2D>();
                GetTree().Root.GetNode("./GameArea").CallDeferred("add_child", entity);
                debugEntities.Add(entity);
                entity.Position = new Vector2(-100, -100);
            }
           
        }
        base._Ready();
    }

    public override int GetMoveDirection(Vector2 ballPosition, float aiYPosition, Rect2 viewportRect)
    {
        if (isDebug)
        {
            foreach (var item in debugEntities)
            {
                item.Position = new Vector2(-100, -100);
                
            }
        }
        Vector2 velocity = ballPosition - lastBallPosition;
        if (lastBallPosition == Vector2.Zero)
        {
            velocity = Vector2.Zero;
        }
        bool movingTowards = velocity.X > 0 ? true : false;
        lastBallPosition = ballPosition;
        if (!movingTowards || viewportRect.Size.X / screenSplitAmount * (screenSplitAmount - 1) < ballPosition.X)
        {            
            return subStrategy.GetMoveDirection(ballPosition, aiYPosition, viewportRect);
        }

        Vector2 predictedBallPosition = ballPosition;
        Vector2 finalHit = Vector2.Zero;
        for (int i = 0; i < predictions; i++)
        {
            Vector2 currentPrediction = PredictHitPoint(predictedBallPosition, velocity, viewportRect.Size);
            finalHit = currentPrediction;
            if (currentPrediction.X > viewportRect.Size.X)
            {
                break;
            }
            velocity = new Vector2(velocity.X, velocity.Y *  -1);
            predictedBallPosition = currentPrediction;
            if (isDebug)
            {
                debugEntities[i].Position = currentPrediction;    
            }
        }

        float angle = velocity.Angle();
        float lastAngle = (float)(Math.PI - angle - Math.PI / 2);
        float distance = Math.Abs(viewportRect.Size.X - finalHit.X);
        float flightDirectionLength = (float)(distance / Math.Sin(lastAngle));
        float baseLine = (float)Math.Sqrt(-Math.Pow(distance, 2) + Math.Pow(flightDirectionLength, 2));
        int turnDistanceAround = velocity.Y < 0 ? -1 : 1;
        var realFinalPoint = new Vector2(finalHit.X - distance, finalHit.Y + baseLine * turnDistanceAround);

        if (isDebug)
        {
            var spawnedDebugEntity = debugEntities.Last();
            spawnedDebugEntity.Position = realFinalPoint;
            spawnedDebugEntity.Position = new Vector2(realFinalPoint.X - 50, realFinalPoint.Y);
        }
        return realFinalPoint.Y < aiYPosition ? -1 : 1;
    }

    Vector2 PredictHitPoint(Vector2 ballPosition, Vector2 velocity, Vector2 viewportSize)
    {
        Vector2 compareVector = velocity.Y < 1 ? new Vector2(0, additionalBorderPixels) : new Vector2(0, viewportSize.Y - additionalBorderPixels);
        float angle = velocity.Angle();
        float lastCornerAngle = (float)(Math.PI - angle - Math.PI / 2);
        float distanceSide = Math.Abs(ballPosition.Y - compareVector.Y);
        float flightDirectionLength = (float)(distanceSide / Math.Sin(lastCornerAngle));
        float baseLine = (float)Math.Sqrt(-Math.Pow(distanceSide, 2) + Math.Pow(flightDirectionLength, 2));
        int turnDistanceAround = ballPosition.Y > compareVector.Y ? -1 : 1;
        return new Vector2(ballPosition.X + baseLine, ballPosition.Y + distanceSide * turnDistanceAround);
    }
}