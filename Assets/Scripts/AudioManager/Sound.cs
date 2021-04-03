using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume = 0.7f;
    [Range(0.1f, 3f)]
    public float pitch = 1f;
    [Range(0f, 0.5f)]
    public float randomVolume = 0.1f;
    [Range(0f, 0.5f)]
    public float randomPitch = 0.1f;
    public bool loop;

    public bool playedConstently = false;

    private AudioSource source;

    public void SetSource(AudioSource _source)
	{
		source = _source;
        source.clip = clip;
	}

    public void Play()
	{
        source.volume = volume * (1 + Random.Range(-randomVolume / 2, randomVolume / 2));
        source.pitch = pitch * (1 + Random.Range(-randomPitch / 2, randomPitch / 2));
        source.loop = loop;
        source.Play();
	}

}