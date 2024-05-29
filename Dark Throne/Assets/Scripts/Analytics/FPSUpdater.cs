using UnityEngine;
using TMPro;



public class FPSUpdater : MonoBehaviour 
{
    float fps;
    [SerializeField] float updateFrequency = 0.2f;
    float updateTimer;

    [SerializeField] TextMeshProUGUI fpsTitle;
    private void UpdateFPSDisplay()
    {
        updateTimer -= Time.deltaTime;
        if (updateTimer <= 0f){
            fps = 1f / Time.unscaledDeltaTime;
            fpsTitle.text = "FPS: " + Mathf.Round(fps);
            updateTimer = updateFrequency;
        }

    }

    void Update()
    {
        UpdateFPSDisplay();
    }
    void Start(){
        updateTimer = updateFrequency;
    }
}