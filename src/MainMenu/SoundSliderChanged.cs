using Godot;
using System;
using System.Diagnostics;

public partial class SoundSliderChanged : VSlider
{
	[Export(PropertyHint.Range, "-80, 0")]
	private float minDb = -80;
	
	[Export(PropertyHint.Range, "0, 8")]
	private float maxDb = 0;

	[Export]
	private AudioStreamMP3 exampleSound;

	[Export]
	private AudioStreamPlayer2D soundPlayer;

	[Export]
	private string audioBusName;

	[Export]
	private Timer changedTimer;

	[Export]
	private bool shouldHaveInitialFocus = false;

	private int busIndex;

	private double timerTime;


    public override void _Ready()
    {
        busIndex = AudioServer.GetBusIndex(audioBusName);
		if (changedTimer is not null)
		{
			changedTimer.Timeout += TimerRunOut;
			changedTimer.OneShot = true;
			timerTime = changedTimer.WaitTime;
		}

		Value = GetAudioPercentage(busIndex);
		if (shouldHaveInitialFocus)
		{
			GrabFocus();
		}
		
		ValueChanged += ValueHasChanged;
		FocusExited += FocusLost;
    }

    private void TimerRunOut()
    {
		if (soundPlayer is null)
		{
			return;
		}
		soundPlayer.Stream = exampleSound;
		soundPlayer.Play();
    }


    private void FocusLost()
    {
		changedTimer.Stop();
		changedTimer.WaitTime = timerTime;
    }


    private void ValueHasChanged(double value)
    {
		SetBusVolume((float)Value);
		if (changedTimer is not null && changedTimer.TimeLeft == 0)
		{
			changedTimer.WaitTime = timerTime;
			changedTimer.Start();
		}
    }	

	private int GetAudioPercentage(int busId)
	{
		float totalRange = Math.Abs(minDb) + Math.Abs(maxDb);
		float currentVolume = AudioServer.GetBusVolumeDb(busId);
		currentVolume += Math.Abs(minDb);
		float percentage = currentVolume / totalRange;
		return (int)(percentage * 100);
	} 

	private void SetBusVolume(float volume)
	{
		volume /= 100;
		float totalRange = Math.Abs(minDb) + Math.Abs(maxDb);
		float target = totalRange * volume;
		Debug.WriteLine(target - Math.Abs(minDb));
		AudioServer.SetBusVolumeDb(busIndex, target - Math.Abs(minDb));
	}
}
