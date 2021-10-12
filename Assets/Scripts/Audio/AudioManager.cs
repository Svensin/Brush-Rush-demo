using UnityEngine.Audio;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Utilities.SaversLoaders;

public class AudioManager : SingletonComponent<AudioManager>
{
	/// <summary>
	/// current mixer group
	/// </summary>
	public AudioMixerGroup mixerGroup;

	/// <summary>
	/// all sound effects in game
	/// </summary>
	public Sound[] sounds;

	/// <summary>
	/// main music theme of the game
	/// </summary>
	public Sound theme;

	/// <summary>
	/// if theme should be playing
	/// </summary>
	private bool isMusicPlaying = true;

	/// <summary>
	/// if sound effects should be playing
	/// </summary>
	private bool areSoundsPlaying = true;

	/// <summary>
	/// To activate or deactivate music theme. Can be called only from AudioSaverLoader class
	/// </summary>
	/// <param name="sender">what object calls this method</param>
	/// <param name="value"> if music theme should be played</param>
	public void SetMusic(object sender, bool value)
	{
		if (sender is AudioSaverLoader)
		{
			isMusicPlaying = value;
		}
	}

	/// <summary>
	/// To activate or deactivate music theme. Can be called only from AudioSaverLoader class
	/// </summary>
	/// <param name="sender">what object calls this method</param>
	/// <param name="value"> if sound effect should be played</param>
	public void SetSounds(object sender, bool value)
	{
		if (sender is AudioSaverLoader)
		{
			areSoundsPlaying = value;
		}
	}

	

	void Awake()
	{
		DontDestroyOnLoad(this);
		foreach (Sound s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;

			s.source.outputAudioMixerGroup = mixerGroup;
		}

		if (theme.source == null)
		{
			InitTheme();
		}
	}

	/// <summary>
	/// Initializes theme music variables from the inspector and starts to play if needed
	/// </summary>
	private void InitTheme()
	{
		theme.source = gameObject.AddComponent<AudioSource>();

		theme.source.clip = theme.clip;
		theme.source.loop = theme.loop;

		theme.source.outputAudioMixerGroup = mixerGroup;

		
		theme.source.Play();

		if (!isMusicPlaying)
		{
			theme.source.Pause();
		}
	}

	/// <summary>
	/// plays wanted sound effect
	/// </summary>
	/// <param name="sound">wanted sound effect name</param>
	public void PlaySoundEffect(string sound)
	{
		if(areSoundsPlaying)
        {
	        Sound s = Array.Find(sounds, item => item.name == sound);
			if (s == null)
			{
				Debug.LogWarning("Sound: " + name + " not found!");
				return;
			}

			s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
			s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));
			
			s.source.Play();
		}
	}

	/// <summary>
	/// stops playing wanted sound effect
	/// </summary>
	/// <param name="sound">wanted sound effect name</param>
	public void StopPlaying(string sound)
	{
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}

		s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
		s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

		s.source.Stop();
	}

	/// <summary>
	/// Switches theme playing state. E.g. if music plays, it stops and vice versa
	/// </summary>
	public void SwitchThemePlayingState()
    {
		isMusicPlaying = !isMusicPlaying;

		if (theme.source == null)
		{
			InitTheme();
		}
		
		if(isMusicPlaying)
        {
	        theme.source.UnPause();
        }
		else
        {
			theme.source.Pause();
        }
    }

	/// <summary>
	/// Switches sound effects playing state. E.g. if sound effect are playing, it stops and vice versa
	/// </summary>
	public void SwitchSoundEffectsPlaying()
    {
		areSoundsPlaying = !areSoundsPlaying;
    }
}
