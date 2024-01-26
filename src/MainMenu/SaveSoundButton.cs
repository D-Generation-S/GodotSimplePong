using Godot;
using System.Diagnostics;

public partial class SaveSoundButton : Button
{
	[Signal]
	public delegate void CanCloseNowEventHandler();

	[Export]
	private string savePath = "user://audio_settings.res";

    public override void _Pressed()
    {	
		var audio_settings = new AudioSettingResource(GetVolume("Master"), GetVolume("Effects"), GetVolume("Music"));
		var response = ResourceSaver.Save(audio_settings, savePath);
		if (response != Error.Ok)
		{
			Debug.Fail(response.ToString());
		}
		EmitSignal(SignalName.CanCloseNow);
    }

	private float GetVolume(string busName)
	{
		var id = AudioServer.GetBusIndex(busName);
		return AudioServer.GetBusVolumeDb(id);
	}
}
