using UnityEngine;

public class MapDisplay : MonoBehaviour
{
    public GameObject mapPanel;

    void Start()
    {
        // Automatically find and assign the map panel
        if (mapPanel == null)
        {
            Transform mapTransform = transform.Find("Map");
            if (mapTransform != null)
            {
                mapPanel = mapTransform.gameObject;
            }
            else
            {
                Debug.LogError("MapDisplay: No child GameObject named 'Map' found. Please check your hierarchy.");
            }
        }

        // Optionally, start with the map not visible
        if (mapPanel != null)
        {
            Time.timeScale = 1; // Ensure time is running normally when the game starts
            mapPanel.SetActive(false);
        }
        else
        {
            Debug.LogError("MapDisplay: Map panel not assigned and cannot be found.");
        }
    }

    void Update()
    {
        if (mapPanel != null && Input.GetKeyDown(KeyCode.Tab))
        {
            mapPanel.SetActive(!mapPanel.activeSelf);

            // Toggle time scale between paused and running
            if (mapPanel.activeSelf)
            {
                Time.timeScale = 0; // Pause the game when the map is visible
            }
            else
            {
                Time.timeScale = 1; // Resume normal game speed when the map is hidden
            }
        }
    }
}
