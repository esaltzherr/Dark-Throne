using System.Collections;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private TrailRenderer tr;

    public bool IsDashing { get; private set; }
    private bool dashAquired = false;
    private bool canDash = true;
    private float dashingPower = 24f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;

    void Start()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
            if (rb == null)
            {
                Debug.LogError("Rigidbody2D component not found on " + gameObject.name);
            }
        }

        if (tr == null)
        {
            tr = GetComponent<TrailRenderer>();
            if (tr == null)
            {
                Debug.LogWarning("TrailRenderer component not found on " + gameObject.name + ". Dashing visual effects will not be visible.");
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash && dashAquired)
        {
            StartCoroutine(Dash());
        }
        if (Input.GetKeyDown(KeyCode.M)){
            toggleDash();
        }
    }

    private IEnumerator Dash()
    {
        IsDashing = true;
        canDash = false;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0;

        float horizontal = Input.GetAxisRaw("Horizontal");
        if (horizontal == 0)
        {
            horizontal = Mathf.Sign(transform.localScale.x);
        }

        rb.velocity = new Vector2(Mathf.Sign(horizontal) * Mathf.Abs(transform.localScale.x) * dashingPower, 0f);
        if (tr != null)
        {
            tr.emitting = true;
        }

        yield return new WaitForSeconds(dashingTime);

        if (tr != null)
        {
            tr.emitting = false;
        }
        rb.gravityScale = originalGravity;
        IsDashing = false;

        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    public void gainDash(){
        dashAquired = true;
    }

    public void loseDash(){
        dashAquired = false;
    }

    public void toggleDash(){
        if (dashAquired){
            loseDash();
        }
        else{
            gainDash();
        }
    }
}
