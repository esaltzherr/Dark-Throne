using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class CameraSlide : MonoBehaviour
{
    // Start is called before the first frame update
    public SpriteRenderer player;
    public Vector2 inbetweenSpace = new Vector2(19.9f, 6f);
    public Vector2 destination;
    public float speed = 10;
    public bool moving = false;
    public Bounds cameraBounds;

    private PlayerMovement playerMovementScript;

    public GameManager gameManager;

    void Start()
    {
        cameraBounds = this.GetComponent<Collider2D>().bounds;
        playerMovementScript = player.GetComponent<PlayerMovement>();

        AdjustCameraToColliderSize(); // Adjust the BoxCollider2D to match the camera's view
        // player = GetComponent<SpriteRenderer>();
        // Debug.Log(player.size);
    }

    // Update is called once per frame
    void Update()
    {


    }


    private void AdjustCameraToColliderSize()
    {
        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
        if (boxCollider != null)
        {
            // Calculate the desired orthographic size based on the collider's size
            float desiredHeight = boxCollider.size.y / 2; // Divide by 2 because orthographic size is half the height
            Camera.main.orthographicSize = desiredHeight;

            // Optionally, adjust the camera width by setting the aspect ratio, but usually, you would leave this to automatically adjust based on the device's screen
            // float desiredAspectRatio = boxCollider.size.x / boxCollider.size.y;
            // Camera.main.aspect = desiredAspectRatio; // Only set this if you want a fixed aspect ratio
        }
        else
        {
            Debug.LogWarning("BoxCollider2D component not found on the GameObject.");
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

            Debug.Log("Moving Camera");
            cameraBounds = this.GetComponent<Collider2D>().bounds;
            Vector2 sizeOfScreen = new Vector2(MathF.Abs(cameraBounds.max.x - cameraBounds.min.x), MathF.Abs(cameraBounds.max.y - cameraBounds.min.y));
            Vector3 movementDirection = Vector3.zero; // Initialize as zero vector for 3D movement

            // Determine the direction for the movement
            if (other.bounds.min.x >= cameraBounds.max.x)
            {
                movementDirection = Vector3.right * sizeOfScreen.x;
            }
            else if (other.bounds.max.x <= cameraBounds.min.x)
            {
                movementDirection = Vector3.left * sizeOfScreen.x;
            }
            else if (other.bounds.max.y <= cameraBounds.min.y)
            {
                movementDirection = Vector3.down * sizeOfScreen.y;
            }
            else if (other.bounds.min.y >= cameraBounds.max.y)
            {
                movementDirection = Vector3.up * sizeOfScreen.y;
            }

            // Ensure the Z-axis remains unchanged
            float currentZ = transform.position.z;

            // Calculate the actual target position by adding the movement direction to the current position
            Vector3 targetPosition = transform.position + movementDirection;

            // Preserve the original Z-axis value
            targetPosition.z = currentZ;



            // playerRB.playerDisabled = false; ------------------------------


            gameManager.Pause();
            // Start the coroutine to move the camera smoothly to the new position
            StartCoroutine(MoveCameraSmoothly(targetPosition, 1f)); // Adjust the duration as needed

        }
    }


    private IEnumerator MoveCameraSmoothly(Vector3 targetPosition, float duration)
    {
        float elapsedTime = 0;
        Vector3 startPosition = transform.position;

        while (elapsedTime < duration)
        {
            // Use Time.unscaledDeltaTime instead of Time.deltaTime
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.unscaledDeltaTime; // This is the key change
            yield return null; // Wait for the next frame
        }

        transform.position = targetPosition; // Ensure the camera is exactly at the target position at the end

        gameManager.Unpause();
    }




}