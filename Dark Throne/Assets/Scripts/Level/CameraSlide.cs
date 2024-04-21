using System;
using System.Collections;
using UnityEngine;

public class CameraSlide : MonoBehaviour
{
    public SpriteRenderer player;
    public Vector2 inbetweenSpace = new Vector2(19.9f, 6f);
    public Vector2 destination;
    public float speed = 10;
    public bool moving = false;
    public Bounds cameraBounds;

    private PlayerMovement playerMovementScript;
    public GameManager gameManager;
    public float cameraMargin = 1.0f;

    void Start()
    {
        AdjustCameraColliderSize();
        cameraBounds = GetComponent<Collider2D>().bounds;



        // Find and assign the player
        GameObject playerGameObject = GameObject.FindGameObjectWithTag("Player");
        if (playerGameObject != null)
        {
            player = playerGameObject.GetComponent<SpriteRenderer>();
            if (player == null) // Check if the player GameObject has a SpriteRenderer component
            {
                Debug.LogWarning("The player GameObject does not have a SpriteRenderer component.");
            }
        }
        else
        {
            Debug.LogWarning("Player GameObject not found. Make sure your player GameObject is tagged with 'Player'.");
        }
    }

    void Update()
    {
        // Intentionally left blank as per requirement to not constantly follow the player
        CheckPlayerBounds();
    }


    private void CheckPlayerBounds()
    {
        if (player == null) return; // Do nothing if the player has not been assigned.

        // Update the camera bounds since they might have changed.
        cameraBounds = GetComponent<Collider2D>().bounds;
        Vector2 sizeOfScreen = new Vector2(Mathf.Abs(cameraBounds.max.x - cameraBounds.min.x), Mathf.Abs(cameraBounds.max.y - cameraBounds.min.y));

        Vector3 playerPosition = player.transform.position; // Player's world position

        // Calculate how many full camera widths and heights away the player is from the camera center.
        float horizontalDistance = playerPosition.x - cameraBounds.center.x;
        float verticalDistance = playerPosition.y - cameraBounds.center.y;

        // Calculate how many full screens away the player is horizontally and vertically.
        int horizontalScreensAway = Mathf.FloorToInt(horizontalDistance / sizeOfScreen.x);
        int verticalScreensAway = Mathf.FloorToInt(verticalDistance / sizeOfScreen.y);

        // Only update the camera if the player is more than half a screen away in either direction.
        if (Mathf.Abs(horizontalDistance / sizeOfScreen.x) >= 1.1 || Mathf.Abs(verticalDistance / sizeOfScreen.y) >= 1.1)
        {
            Debug.Log("Player is out of camera bounds.");
            // Calculate the new target position for the camera based on the number of screens away.
            Vector3 targetPosition = new Vector3(
                cameraBounds.center.x + horizontalScreensAway * sizeOfScreen.x,
                cameraBounds.center.y + verticalScreensAway * sizeOfScreen.y,
                transform.position.z // Keep the camera's current Z coordinate
            );

            // Move the camera smoothly to the new position
            if (Camera.main.gameObject.activeSelf)
            {
                StartCoroutine(MoveCameraSmoothly(targetPosition, 0f)); // You can adjust the duration as needed
            }
        }
    }

    private void AdjustCameraColliderSize()
    {
        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
        if (boxCollider != null)
        {
            Camera mainCamera = Camera.main;
            if (mainCamera != null)
            {
                float aspectRatio = mainCamera.aspect;
                float verticalSize = mainCamera.orthographicSize;
                float horizontalSize = aspectRatio * verticalSize;

                // Add margin
                float marginHorizontal = cameraMargin * 15;
                float marginVertical = cameraMargin * 8;

                // Set collider size
                boxCollider.size = new Vector2(horizontalSize + marginHorizontal, verticalSize + marginVertical);
            }
            else
            {
                Debug.LogWarning("Main camera not found.");
            }
        }
        else
        {
            Debug.LogWarning("BoxCollider2D component not found on the GameObject.");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && Application.isPlaying)
        {
            cameraBounds = GetComponent<Collider2D>().bounds;
            Vector2 sizeOfScreen = new Vector2(Mathf.Abs(cameraBounds.max.x - cameraBounds.min.x), Mathf.Abs(cameraBounds.max.y - cameraBounds.min.y));
            Vector3 movementDirection = Vector3.zero;

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

            float currentZ = transform.position.z;
            Vector3 targetPosition = transform.position + movementDirection;
            targetPosition.z = currentZ;

            gameManager.Pause();
            if (Camera.main.gameObject.activeSelf)
            {
                StartCoroutine(MoveCameraSmoothly(targetPosition, 0.5f));
            }
        }
    }

    private IEnumerator MoveCameraSmoothly(Vector3 targetPosition, float duration)
    {
        Camera mainCamera = Camera.main;
        if (mainCamera != null && mainCamera.gameObject.activeSelf)
        {
            float elapsedTime = 0;
            Vector3 startPosition = transform.position;

            while (elapsedTime < duration)
            {
                transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
                elapsedTime += Time.unscaledDeltaTime;
                yield return null;
            }

            transform.position = targetPosition;
        }
        else
        {
            Debug.LogWarning("Main camera is inactive or not found. Coroutine cannot be started.");
        }

        gameManager.Unpause();
    }
}
