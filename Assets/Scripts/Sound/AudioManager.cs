using Unity.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using System;
using UnityEngine;


public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    [Range(0, 1)]
    public float volume;
    public bool isMusicEnabled = true;
    public bool isSoundEnabled = true;
    private List<Sound> music = new List<Sound>();
    public List<string> songs = new List<string>();
    int songIndex = 0;

    private bool setupDone = false;

    public static AudioManager Singleton;

    void Awake()
    {
        if (Singleton == null)
        {
            Singleton = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        if (!setupDone)
            Setup();
    }

    void Setup()
    {
        DontDestroyOnLoad(this.gameObject);
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume * volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            if (s.isMusic)
            {
                music.Add(s);
            }
        }
        setupDone = true;
    }

    bool MusicIsPlaying()
    {
        foreach (Sound s in music)
        {
            if (s.source.isPlaying) return true;
        }
        return false;
    }

    public void PlayNextSong()
    {
        if (!MusicIsPlaying())
        {
            if (songIndex < songs.Count)
            {
                Play((songs[songIndex]));
                songIndex++;
            }
            else if (songs.Count == 0)
            {
                return;
            }
            else
            {
                songIndex = 0;
                PlayNextSong();
            }
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning($"Sound: {name} not found!");
            return;
        }
        if (s.isMusic && (!isMusicEnabled || MusicIsPlaying()))
        {
            return;
        }
        else if (isSoundEnabled)
        {
            s.source.Play();
        }
    }

    public void DisableMusic()
    {
        isMusicEnabled = false;
        PauseAll();
    }

    public void EnableMusic()
    {
        isMusicEnabled = true;
        ResumeAll();
    }

    public void StopAll()
    {
        foreach (Sound s in sounds)
        {
            s.source.Stop();
        }
    }

    public void PauseAll()
    {
        foreach (Sound s in sounds)
        {
            s.source.Pause();
        }
    }

    public void ResumeAll()
    {
        foreach (Sound s in sounds)
        {
            s.source.UnPause();
        }
    }
}
