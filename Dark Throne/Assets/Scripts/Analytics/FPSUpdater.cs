using UnityEngine;
using TMPro;



public class FPSUpdater : MonoBehaviour
{
    float fps;
    [SerializeField] float updateFrequency = 0.2f;
    float updateTimer;
    private static FPSUpdater instance;


    [SerializeField] TextMeshProUGUI fpsTitle;
    private void UpdateFPSDisplay()
    {
        updateTimer -= Time.deltaTime;
        if (updateTimer <= 0f)
        {
            fps = 1f / Time.unscaledDeltaTime;
            fpsTitle.text = "FPS: " + Mathf.Round(fps);
            updateTimer = updateFrequency;
        }

    }

    void Update()
    {
        UpdateFPSDisplay();
    }
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(transform.root.gameObject);
            updateTimer = updateFrequency;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}