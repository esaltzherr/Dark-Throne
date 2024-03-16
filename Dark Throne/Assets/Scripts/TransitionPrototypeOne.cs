using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TransitionPrototypeOne : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 3f;
    public Button button;
    private float timer;

    // Start is called before the first frame update

    void Start()
    {
        timer = 0f;
    }
    IEnumerator FadeRoutine(string sceneName)
    {
        fadeImage.gameObject.SetActive(true);
        Color color = fadeImage.color;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            color.a = Mathf.Clamp01((float)timer / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }

        SceneManager.LoadScene(sceneName);

        timer = 0f;
        while(timer < fadeDuration)
        {
            timer += Time.deltaTime;
            color.a = 1f - Mathf.Clamp01(timer / fadeDuration);
            fadeImage.color = color;
            yield return null;

        }

        fadeImage.gameObject.SetActive(false); 
    }

    public void OnPointerClick()
    {
        StartCoroutine(FadeRoutine("CaveLevel"));
    }
}
