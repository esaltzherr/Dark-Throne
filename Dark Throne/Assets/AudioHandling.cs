using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class AudioHandling : MonoBehaviour
{
    public Slider volumeSlider;

    private void Start()
    {
        AudioListener.volume = 0.5f;
        volumeSlider.value = AudioListener.volume;

    }

    // Update is called once per frame
    void Update()
    {
        SetGameVolume(volumeSlider.value);
       
    }

    public void SetGameVolume(float volume)
    {
        AudioListener.volume = volume;
    }
}
