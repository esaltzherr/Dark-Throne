using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGameFadeTransition : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 3f;
    public Button button;
    public string nextScene;
    private float timer;

    public SaveLoadJSONPlayer playerJsonScript;

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

        SceneManager.LoadSceneAsync(sceneName);
        LoadData();



        timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            color.a = 1f - Mathf.Clamp01(timer / fadeDuration);
            fadeImage.color = color;
            yield return null;

        }

        fadeImage.gameObject.SetActive(false);
        timer = 0f;
    }

    public void OnPointerClick()
    {


        StartCoroutine(FadeRoutine(nextScene));
    }

    public void LoadData()
    {
        playerJsonScript = FindObjectOfType<SaveLoadJSONPlayer>();

        if (playerJsonScript != null)
        {
            playerJsonScript.LoadGame(); // Load the game data
        }
        else
        {
            Debug.LogError("SaveLoadJSONPlayer script is not assigned!");
        }

    }
}
