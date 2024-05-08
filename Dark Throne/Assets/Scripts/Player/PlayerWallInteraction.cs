using UnityEngine;

public class PlayerWallInteraction : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform wallCheck;
    public bool IsWallSliding { get; private set; }
    public bool IsWallJumping { get; private set; }
    private float wallSlidingSpeed = 2f;
    private Vector2 wallJumpingPower = new Vector2(8f, 12f);
    private float wallJumpingDuration = 0.4f;

    private PlayerMovement playerMovement; // Reference to the PlayerMovement script
    public KeyCode jumpKey = KeyCode.W;

    void Start()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
            if (rb == null)
            {
                Debug.LogError("Rigidbody2D component not found on " + gameObject.name + ". Please attach a Rigidbody2D component.", this);
            }
        }

        playerMovement = GetComponent<PlayerMovement>();
        if (playerMovement == null)
        {
            Debug.LogError("PlayerMovement component not found on " + gameObject.name + ". Please attach a PlayerMovement component.", this);
        }

        if (wallCheck == null){
            wallCheck = transform.Find("WallCheck");
            if (wallCheck == null)
            {
                Debug.LogError("WallCheck transform not found as a child of " + gameObject.name + ". Please ensure there is a child GameObject named 'WallCheck'.", this);
            }
        }
    }

    void Update()
    {
        WallSlide();
        WallJump();
    }

    private bool IsWalled()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(wallCheck.position, 0.2f);
        foreach (var collider in colliders)
        {
            if (collider.CompareTag("Ground"))
            {
                return true;
            }
        }
        return false;
    }

    private void WallSlide()
    {
        if (IsWalled() && !playerMovement.IsGrounded() && Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0)
        {
            IsWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        else
        {
            IsWallSliding = false;
        }
    }

    private void WallJump()
    {   
        if (IsWallSliding && Input.GetKeyDown(jumpKey))
        {
            float wallJumpingDirection = playerMovement.isFacingRight ? -1 : 1;
            IsWallJumping = true;
            rb.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            Invoke(nameof(StopWallJumping), wallJumpingDuration);
        }
    }

    private void StopWallJumping()
    {
        IsWallJumping = false;
    }
}
