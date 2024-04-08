using UnityEngine;

public class PlayerPowerUps : MonoBehaviour
{
    private bool doubleJumpAvailable = false;
    [SerializeField] private Canvas powerUpCanvas;

    void Start()
    {
        if (powerUpCanvas == null)
        {
            powerUpCanvas = GameObject.FindGameObjectWithTag("PowerUpTag").GetComponent<Canvas>();
            if (powerUpCanvas == null){
                Debug.LogError("PowerUpCanvas is not assigned on " + gameObject.name);
            }
            else{
                powerUpCanvas.enabled = false;
            }
            
        }
        else
        {
            powerUpCanvas.enabled = false;
        }

    }

    // Example collision detection for power-up activation
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PowerUp"))
        {
            doubleJumpAvailable = true;
            if (powerUpCanvas != null)
            {
                powerUpCanvas.enabled = true;
            }
        }
    }

    public bool CanDoubleJump() 
    {
        return doubleJumpAvailable;
    }
}
