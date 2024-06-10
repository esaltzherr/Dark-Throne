using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    public AudioClip dashing_sound;
    public AudioClip deep;
    public AudioClip Boss;
    public AudioClip footstep_1;
    public AudioClip heavy_hit;
    public AudioClip heavy_hit_2;
    public AudioClip new_level;
    public AudioClip player_hit;
    public AudioClip power_up;
    public AudioClip stab;
    public AudioClip enemydie;
    public AudioClip execute;
    public AudioClip playerwalking;
    public AudioClip jump;

    private static AudioManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }

        if (SFXSource == null)
        {
            SFXSource = GetComponent<AudioSource>();
        }

        Debug.Log("AudioManager Initialized");
    }

    public void Player_Dash()
    {
        SFXSource.PlayOneShot(dashing_sound);
    }

    public void heavyhit()
    {
        SFXSource.PlayOneShot(heavy_hit);
    }

    public void heavyhit2()
    {
        SFXSource.PlayOneShot(heavy_hit_2);
    }

    public void playerhit()
    {
        SFXSource.PlayOneShot(player_hit);
    }

    public void playerwalk()
    {
        SFXSource.PlayOneShot(playerwalking);
    }

    public void playerjump()
    {
        SFXSource.PlayOneShot(jump);
    }

    public void enemuydie()
    {
        SFXSource.PlayOneShot(enemydie);
    }

    public void enemy_execute()
    {
        SFXSource.PlayOneShot(execute);
    }

}

