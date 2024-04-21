using UnityEngine;

public class SceneIndicator : MonoBehaviour
{
    public GameObject objectToChangeColor; // Assign in inspector
    private Color greyColor = new Color(0.5f, 0.5f, 0.5f, 0.5f); // Grey color
    private Color transparentColor = new Color(1f, 1f, 1f, 0f); // Transparent color

    void Start()
    {
        // If objectToChangeColor is not assigned, use the GameObject this script is attached to
        if (objectToChangeColor == null)
        {
            objectToChangeColor = gameObject;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Change color to grey when player enters trigger
            SetColor(greyColor);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Change color back to transparent when player exits trigger
            SetColor(transparentColor);
        }
    }

    void SetColor(Color color)
    {
        // Check if the object has a SpriteRenderer
        if (objectToChangeColor.GetComponent<SpriteRenderer>() != null)
        {
            objectToChangeColor.GetComponent<SpriteRenderer>().color = color;
        }
        // If not, then check if it has a Renderer component
        else if (objectToChangeColor.GetComponent<Renderer>() != null)
        {
            objectToChangeColor.GetComponent<Renderer>().material.color = color;
        }
    }
}
