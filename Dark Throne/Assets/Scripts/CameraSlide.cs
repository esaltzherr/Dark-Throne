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
    void Start()
    {
        cameraBounds = this.GetComponent<Collider2D>().bounds;
        // player = GetComponent<SpriteRenderer>();
        // Debug.Log(player.size);
    }

    // Update is called once per frame
    void Update()
    {


    }

    // void OnCollisionExit2D(Collision2D other) {
    //     Debug.Log(other.transform.name);
    //     // if other == player 
    // }
    // private void OnTriggerExit2D(Collider2D other)
    // {

    //     Debug.Log(other.transform.tag);
    //     cameraBounds = this.GetComponent<Collider2D>().bounds;
    //     if (other.CompareTag("Player"))
    //     {
    //         Debug.Log("Moving Camera");

    //         moving = true;
    //         Vector2 sizeOfScreen = new Vector2(MathF.Abs(cameraBounds.max.x - cameraBounds.min.x), MathF.Abs(cameraBounds.max.y - cameraBounds.min.y));
    //         // Move off the right
    //         if (other.bounds.min.x >= cameraBounds.max.x)
    //         {
    //             destination = Vector2.right * sizeOfScreen.x;
    //             // transform.Translate(Vector2.right * sizeOfScreen.x);
    //             // move the player a LITTLE bit extra to make sure they dont keep overlapping with the collider
    //             other.transform.Translate(Vector2.right * .1f);

    //         }
    //         // Move off the left
    //         else if (other.bounds.max.x <= cameraBounds.min.x)
    //         {
    //             destination = Vector2.right * -sizeOfScreen.x;

    //             transform.Translate(Vector2.right * -sizeOfScreen.x);
    //             // move the player a LITTLE bit extra to make sure they dont keep overlapping with the collider
    //             other.transform.Translate(Vector2.right * -.1f);
    //         }
    //         // Move off the up
    //         else if (other.bounds.max.y <= cameraBounds.min.y)
    //         {
    //             destination = Vector2.up * -sizeOfScreen.y;
    //             // transform.Translate(Vector2.up * -sizeOfScreen.y);
    //             // move the player a LITTLE bit extra to make sure they dont keep overlapping with the collider
    //             other.transform.Translate(Vector2.up * -.1f);
    //         }
    //         // Move off the down
    //         else if (other.bounds.min.y >= cameraBounds.max.y)
    //         {
    //             destination = Vector2.up * sizeOfScreen.y;
    //             // transform.Translate(Vector2.up * sizeOfScreen.y);
    //             // move the player a LITTLE bit extra to make sure they dont keep overlapping with the collider
    //             other.transform.Translate(Vector2.up * .1f);
    //         }


    //         Debug.Log(destination);


    //         // Probably Pause the game so enemies arent moving and you dont keep moving, maybe not for more seemless.

    //     }

    // }


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
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        transform.position = targetPosition; // Ensure the camera is exactly at the target position at the end
    }



}