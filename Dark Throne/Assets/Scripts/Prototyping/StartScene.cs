using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    public void OnButtonClick()
    {
        SceneManager.LoadScene("ZhiBin_level_1");
    }

}
