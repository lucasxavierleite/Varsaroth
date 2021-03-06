﻿using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

    // Start is called before the first frame update
    void Awake()
    {
        if(instance == null){
            instance = this;
        }else{
            Destroy(gameObject);
            return;
        }


        foreach (Sound s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
			s.playInTransition = false;
        }
        
    }

    public void Play (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null){
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }    
        s.source.Play();
    }

    public bool isPlaying (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null){
            Debug.LogWarning("Sound: " + name + " not found!");
            return false;
        }else if(s.source.isPlaying) {
            return true;
        }else {
            return false;
        }
    }

    public void Stop (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null){
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Stop();
    }

	public void StopAllExcept (string name)
    {
        foreach (Sound s in sounds)
        {
            if (s.name != name)
            {
                s.source.Stop();
            }
        }
    }

    public void Pause (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null){
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.Pause();
    }
    
    public void UnPause (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null){
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.UnPause();
    }    

	public void PauseAll ()
	{
		foreach (Sound s in sounds)
        {
            s.source.Pause();
        }
	}

	public void UnPauseAll ()
	{
		foreach (Sound s in sounds)
        {
            s.source.UnPause();
        }
	}

	public void EnablePlayInTransition (string name)
	{
		Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null){
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.playInTransition = true;
	}

	public void TransitionSounds ()
	{
		foreach (Sound s in sounds)
        {
			if (!s.playInTransition)
            	s.source.Stop();
        }
	}

}
