using System.Data;
using Godot;

public partial class DeleteEmitter : DeleteIfDone
{
    [Export]
    private GpuParticles2D emitterToWatch;

    [Export]
    private AudioStreamPlayer2D playerToWatch;

    public override void _Ready()
    {
        if (!emitterToWatch.Emitting)
        {
            emitterToWatch.Emitting = true;
        }
    }

    public override void _Process(double delta)
    {
        if (deleted || emitterToWatch.Emitting || playerToWatch.Playing)
        {
            return;
        }
        Delete();
    }
}