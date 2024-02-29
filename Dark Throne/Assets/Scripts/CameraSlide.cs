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
        // playerMovementScript = player.GetComponent<PlayerMovement>();

    }

    void Update()
    {
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
            if (Camera.main.gameObject.activeSelf){
                StartCoroutine(MoveCameraSmoothly(targetPosition, 0.5f));
            }
            
            // gameManager.Unpause()

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
