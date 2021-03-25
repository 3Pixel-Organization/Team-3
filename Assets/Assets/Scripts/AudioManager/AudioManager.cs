<<<<<<< Updated upstream
﻿using UnityEngine;
=======
﻿using System;
using UnityEngine;
>>>>>>> Stashed changes

public class AudioManager : MonoBehaviour
{
	public static AudioManager instance;

    public Sound[] sounds;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
<<<<<<< Updated upstream
	}

	private void Start()
    {
		foreach (Sound s in sounds)
		{
            
		}
    }

    public void Play(string name)
	{
		foreach (Sound s in sounds)
		{
			if (s.name == name)
			{
				GameObject _go = new GameObject(s.name + "_Sound");
				_go.transform.SetParent(transform);
				s.SetSource(_go.AddComponent<AudioSource>());

				s.Play();
				Destroy(_go, s.clip.length);
				return;
			}
		}

		Debug.Log("Audio Manager: There is no sound with the name" + name);
=======

		foreach (Sound s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.volume = s.volume;
			s.source.pitch = s.pitch;
		}
	}

	public void Play(string name)
	{
		Sound s = Array.Find(sounds, sound => sound.name == name);
		s.source.Play();
>>>>>>> Stashed changes
	}
}