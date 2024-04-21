using UnityEngine;

public class AdjustGravity : MonoBehaviour
{
    public float gravityScale = 1.0f; // Default gravity scale

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = gravityScale; // Set initial gravity scale
    }

    void OnValidate()
    {
        if (rb != null)
        {
            rb.gravityScale = gravityScale;
        }
    }
}
