using UnityEngine.Audio;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Utilities.SaversLoaders;

public class AudioManager : SingletonComponent<AudioManager>
{

	public AudioMixerGroup mixerGroup;

	public Sound[] sounds;

	public Sound theme;

	private bool isMusicPlaying = true;

	public void SetMusic(object sender, bool value)
	{
		if (sender is AudioSaverLoader)
		{
			isMusicPlaying = value;
		}
	}
	
	public void SetSounds(object sender, bool value)
	{
		if (sender is AudioSaverLoader)
		{
			areSoundsPlaying = value;
		}
	}

	private bool areSoundsPlaying = true;

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

	public void Play(string sound)
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

	// TODO вигадати нормальну назву
	public void ThemePlaying()
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

	// TODO вигадати нормальну назву
	public void SoundsPlaying()
    {
		areSoundsPlaying = !areSoundsPlaying;
    }
}
