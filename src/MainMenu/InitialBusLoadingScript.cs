using Godot;
using System;

public partial class InitialBusLoadingScript : Node
{
	[Export]
	private string[] busNames = new string[] {"Master", "Effects", "Music"};

	private string audioSettingsPath = "user://audio_settings.res";

	public override void _Ready()
	{
		if (ResourceLoader.Exists(audioSettingsPath))
		{
			var volumeSettings = ResourceLoader.Load<AudioSettingResource>(audioSettingsPath, null, ResourceLoader.CacheMode.Ignore);
			if (volumeSettings is not null)
			{
				foreach (var busName in busNames)
				{
					int id = AudioServer.GetBusIndex(busName);
					AudioServer.SetBusVolumeDb(id, volumeSettings.GetAudioByBusName(busName));
				}
			}
		}
	}
}
