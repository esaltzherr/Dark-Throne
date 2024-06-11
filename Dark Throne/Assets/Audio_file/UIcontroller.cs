using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIcontroller : MonoBehaviour
{
    public Slider _musicSlider;

    public void ToggleMusic()
    {
        menu_audiomanager.Instance.ToggleMusic();
        // AudioManager.Instance.ToggleMusic();
    }

    public void MusicVolumn()
    {
        menu_audiomanager.Instance.MusicVolumn(_musicSlider.value);
    }
}
