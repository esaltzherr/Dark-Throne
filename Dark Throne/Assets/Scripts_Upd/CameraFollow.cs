using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Assign your player's transform in the inspector
    public Transform rectangle; // Assign the rectangle's transform here to define bounds

    private Camera cam;
    private float halfHeight;
    private float halfWidth;

    void Start()
    {
        cam = GetComponent<Camera>();
        halfHeight = cam.orthographicSize;
        halfWidth = halfHeight * cam.aspect;

        FindPlayer();
        FindBounds();
    }

    void Update()
    {
        if (player != null && rectangle != null)
        {
            ClampCamera();
        }
    }

    private void FindPlayer()
    {
        if (player == null)
        {
            GameObject playerObject = GameObject.FindWithTag("Player");
            if (playerObject != null)
            {
                player = playerObject.transform;
            }
            else
            {
                Debug.LogWarning("CameraFollow: Player not found. Please assign a Player tag to the player object.");
            }
        }
    }

    private void FindBounds()
    {
        if (rectangle == null)
        {
            GameObject boundsObject = GameObject.Find("CameraBounds");
            if (boundsObject != null)
            {
                rectangle = boundsObject.transform;
            }
            else
            {
                Debug.LogWarning("CameraFollow: Bounds object not found. Please ensure there is a GameObject named 'Bounds'.");
            }
        }
    }

    private void ClampCamera()
    {
        // Assuming the rectangle's local scale is being used to define the bounds
        float minX = rectangle.position.x - rectangle.localScale.x / 2 + halfWidth;
        float maxX = rectangle.position.x + rectangle.localScale.x / 2 - halfWidth;
        float minY = rectangle.position.y - rectangle.localScale.y / 2 + halfHeight;
        float maxY = rectangle.position.y + rectangle.localScale.y / 2 - halfHeight;

        // Follow the player with clamping
        float cameraX = Mathf.Clamp(player.position.x, minX, maxX);
        float cameraY = Mathf.Clamp(player.position.y, minY, maxY);

        // Set the camera position, assuming a 2D setup (ignoring Z axis changes)
        transform.position = new Vector3(cameraX, cameraY, transform.position.z);
    }
}
