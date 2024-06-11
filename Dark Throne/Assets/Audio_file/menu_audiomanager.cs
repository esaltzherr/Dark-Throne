using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class menu_audiomanager : MonoBehaviour
{
    public static menu_audiomanager Instance;

    public sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSources;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayMusic("Theme");
    }

    public void PlayMusic(string name){
        sound s = Array.Find(musicSounds, x => x.name == name);

        if(s == null)
        {
            Debug.Log("Sound Not Found");
        }   
        else{
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }
}

