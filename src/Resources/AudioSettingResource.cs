using Godot;

public partial class AudioSettingResource : Resource
{
    [ExportGroup("Master Volume")]
    [Export]
    private float masterVolume;

    [ExportGroup("Master Volume")]
    [Export]
    private bool masterVolumeMute = false;

    [ExportGroup("Effect Volume")]
    [Export]
    private float effectVolume;

    [ExportGroup("Effect Volume")]
    [Export]
    private bool effectVolumeMute = false;

     [ExportGroup("Music Volume")]
    [Export]
    private float musicVolume;

    [ExportGroup("Music Volume")]
    [Export]
    private bool musicVolumeMute = false;

    public AudioSettingResource() : this(0,0,0)
    {
    }

    public AudioSettingResource(float masterVolume, float effectVolume, float musicVolume)
    {
        this.masterVolume = masterVolume;
        this.effectVolume = effectVolume;
        this.musicVolume = musicVolume;
    }

    public float GetAudioByBusName(string busName)
    {
        return busName switch {
            "Master" => masterVolume,
            "Effects" => effectVolume,
            "Music" => musicVolume,
            _ => int.MinValue
        };
    }
}