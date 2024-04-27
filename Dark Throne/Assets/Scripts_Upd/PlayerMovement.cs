using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private ParticleSystem dust;
    [SerializeField] private Transform groundCheck;

    public float speed = 8f;
    public float jumpingPower = 15f;
    public bool isFacingRight = true;
    private float horizontal;

    // Add a jump key field to customize the jump input in the inspector
    public KeyCode jumpKey = KeyCode.W;
    public PlayerPowerUps playerPowerUps;


    // remove this after playtetst prob / when persitant data
    public static PlayerMovement Instance { get; private set; } // Singleton instance
    private void Awake()
    {
        if (Instance == null)
        {
            // If there is no instance, this becomes the singleton instance of the SpawnManager
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            // A SpawnManager instance already exists, destroy this one
            Destroy(gameObject);
            return;
        }
    }
    // remove
    public void KillPlayer()
    {

        Destroy(gameObject);
        Instance = null;
        return;


    }


    void Start()
    {



        playerPowerUps = GetComponent<PlayerPowerUps>();
        // Ensure Rigidbody2D is assigned
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
            if (rb == null)
            {
                Debug.LogError("Rigidbody2D component not found on " + gameObject.name + ". Please attach a Rigidbody2D component.", this);
            }
        }

        // Ensure Animator is assigned
        if (animator == null)
        {
            animator = GetComponent<Animator>();
            if (animator == null)
            {
                Debug.LogError("Animator component not found on " + gameObject.name + ". Please attach an Animator component.", this);
            }
        }

        // Ensure ParticleSystem is assigned
        if (dust == null)
        {
            dust = GetComponentInChildren<ParticleSystem>();
            if (dust == null)
            {
                Debug.LogError("ParticleSystem component not found on " + gameObject.name + ". Please attach a ParticleSystem component.", this);
            }
        }

        if (groundCheck == null)
        {
            groundCheck = transform.Find("GroundCheck");
            if (groundCheck == null)
            {
                Debug.LogError("groundCheck transform not found as a child of " + gameObject.name + ". Please ensure there is a child GameObject named 'GroundCheck'.", this);
            }
        }

    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("Speed", Mathf.Abs(horizontal));

        if (Input.GetKeyDown(jumpKey) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (!GetComponent<PlayerWallInteraction>().IsWallJumping)
            {
                if (IsGrounded())
                {
                    Jump();
                }
                else if (playerPowerUps.CanDoubleJump())
                {
                    Jump();
                    playerPowerUps.DoubleJump();
                }
            }
        }
        if (IsGrounded())
        {
            playerPowerUps.ResetDoubleJump();
        }

        // if (Input.GetKeyDown(jumpKey) && IsGrounded() && !GetComponent<PlayerWallInteraction>().IsWallJumping)
        // {
        //     Jump();
        // }

        if (!GetComponent<PlayerWallInteraction>().IsWallJumping)
        {
            Flip();
        }
    }

    void FixedUpdate()
    {
        if (!GetComponent<PlayerDash>().IsDashing && !GetComponent<PlayerWallInteraction>().IsWallJumping)
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        }
    }


    public bool IsGrounded()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, 0.2f);
        foreach (var collider in colliders)
        {
            if (collider.CompareTag("Ground"))
            {
                return true;
            }
        }
        return false;
    }

    public void Jump()
    {
        // It's a good idea to check if the player is grounded before allowing a jump in a real game scenario
        rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        CreateDust();
    }

    private void Flip()
    {
        if (horizontal > 0 && !isFacingRight || horizontal < 0 && isFacingRight)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;

            Vector3 dustLocalScale = dust.transform.localScale;
            dustLocalScale.x *= -1f;
            dust.transform.localScale = dustLocalScale;

            dust.Play();
        }
    }

    private void CreateDust()
    {
        dust.Play();
    }
}
