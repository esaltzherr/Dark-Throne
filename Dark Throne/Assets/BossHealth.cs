using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    public Slider healthSlider;
    public int maxHealth = 600;
    private int currentHealth;
    private KeyCode Button = KeyCode.H; // Left mouse button
    private KeyCode SpawnHP = KeyCode.Y;
    public Canvas bossCanvas;
    public Animator animator;


    void Start()
    {
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;
    }

    private void Update()
    {
        healthSlider.value = currentHealth;
        if (Input.GetKeyDown(Button))
        {
            TakeDamage(20);
        }
        if (Input.GetKeyDown(SpawnHP))
        {
            bossCanvas.gameObject.SetActive(true);
        }
    }

    public void TakeDamage(int damage)
    {
        //animator.SetTrigger("Is_Hit");
        currentHealth -= damage;
        //healthSlider.value = currentHealth;

        if (currentHealth <= 0)
        {
            
            this.animator.SetBool("Dead", true);
            this.animator.SetTrigger("Is_Hit");
            Die();
        }
    }

    void Die()
    {
        // add boss death logic here
        this.gameObject.GetComponent<BossMovement>().enabled = false;
        this.gameObject.GetComponent<BossAttack>().enabled = false;
        this.gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
        Rigidbody2D rb = this.gameObject.GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        rb.constraints = RigidbodyConstraints2D.FreezePositionY;

        //disable canvas - hp bar and name
        bossCanvas.gameObject.SetActive(false);
        Debug.Log("Boss died!");
    }
}

