using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public static SoundManager Instance()
    {
        if (instance == null)
        {

            // Create new GameObject and attach the script as a component
            instance = new GameObject().AddComponent<SoundManager>();
            instance.gameObject.name = "SoundManager (Static)";
            instance.gameObject.isStatic = true;
        }

        return instance;
    }

    private void Awake()
    {
        instance = this;
        volume = 1f;
        sources = new List<AudioSource>();
    }

    [SerializeField]
    private AudioSource audioSource;

    List<AudioSource> sources;

    private float volume;
    public float Volume { get { return volume; } set { volume = Mathf.Clamp01(value); } }

    public AudioSource PlaySoundEffect(AudioClip audioClip, Transform parent = null, float volume = 1f, bool loop = false)
    {
        if (audioSource == null)
        {
            Debug.LogError("Sound manager is missing an audio source");
            return null;
        }

        AudioSource source = Instantiate(audioSource, parent);
        source.gameObject.name = audioClip.name;
        source.clip = audioClip;
        source.volume = volume;
        source.loop = loop;
        source.Play();

        if (!loop)
            Destroy(source.gameObject, audioClip.length);

        sources.Add(source);
        return source;
    }

    public void SetVolume(AudioSource source, float volume)
    {
        source.volume = volume * this.volume;
    }

    public void SetPitch(AudioSource source, float pitch)
    {
        source.pitch = pitch;
    }

    public void Destroy(AudioSource source)
    {
        Destroy(source.gameObject);
    }

    public void SetVolume(float volume)
    {
        float oldVolume = this.volume;
        this.volume = volume;

        for (int i = 0; i < sources.Count; i++)
        {
            if (sources[i] == null)
            {
                sources.RemoveAt(i--);
            }
            else
            {
                sources[i].volume = sources[i].volume / oldVolume * volume;
            }
        }
    }
}
