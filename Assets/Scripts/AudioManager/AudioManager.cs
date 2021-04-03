using UnityEngine;
﻿using System;

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
	}

	private void Start()
    {
		foreach (Sound s in sounds)
		{
			if (!s.playedConstently)
			{
				GameObject _go = new GameObject(s.name + "_Sound");
				_go.transform.SetParent(transform);
				s.SetSource(_go.AddComponent<AudioSource>());
			}
		}
    }

    public void Play(string name)
	{
		foreach (Sound s in sounds)
		{
			if (s.name == name && s.playedConstently)
			{
				GameObject _go = new GameObject(s.name + "_Sound");
				_go.transform.SetParent(transform);
				s.SetSource(_go.AddComponent<AudioSource>());

				s.Play();
				Destroy(_go, s.clip.length);
				return;
			}
			else if (s.name == name && !s.playedConstently)
			{
				s.Play();
				return;
			}
		}

		Debug.Log("Audio Manager: There is no sound with the name" + name);
	}
}