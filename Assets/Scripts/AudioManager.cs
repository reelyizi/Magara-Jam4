using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public Sound[] Sounds;

    public static AudioManager instance;

    private void Awake()
    {
        foreach (Sound sound in Sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;

            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;

            sound.source.loop = sound.loop;
        }


        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

    }


    public void AudioPlay(string name)
    {
        Array.Find(Sounds, sound => sound.name == name).source.Play();
    }
    public void AudioStop(string name)
    {
        Array.Find(Sounds, sound => sound.name == name).source.Stop();
    }

    public void TurnOnAllAudio()
    {
        AudioListener.volume = 1;
    }
    public void TurnOffAllAudio()
    {
        AudioListener.volume = 0;
    }
}
