using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth2 : MonoBehaviour
{
    public static int MaxHealth = 100;
    private int currentHealth = MaxHealth;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Animator animator;

    private void Start()
    {

        if (healthSlider == null)
        {
            healthSlider = GetComponent<Slider>();
            if (healthSlider == null)
            {
                Debug.LogError("Slider component not found on " + gameObject.name + ". Please attach a Slider component.", this);
            }
        }
        if (animator == null)
        {
            animator = GetComponent<Animator>();
            if (animator == null)
            {
                Debug.LogError("Animator component not found on " + gameObject.name + ". Please attach a Animator component.", this);
            }
        }




    }

    void Update()
    {
        UpdateHealthUI();

    }

    public int getCurrentHealth()
    {
        return currentHealth;
    }
    public void ChangeHealth(int num)
    {
        if (!(GetComponent<PlayerInvulnerability>().isInvulnerable))
        {
            if (num > 0)
            {
                Heal(num);
            }
            else if (num < 0)
            {
                TakeDamage(-num);
            }
        }
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        Debug.Log("Player took " + damageAmount + " damage. Current health: " + currentHealth);
        animator.SetTrigger("Is_Hit");
        IEnumerator invuln = GetComponent<PlayerInvulnerability>().BecomeInvulnerable();
        StartCoroutine(invuln);
        if (currentHealth <= 0)
        {
            StartCoroutine(Die());
        }
    }

    public void Heal(int healAmount)
    {
        currentHealth = Mathf.Min(currentHealth + healAmount, MaxHealth);
        // Debug.Log("Player healed " + healAmount + " health. Current health: " + currentHealth);
    }


    private void UpdateHealthUI()
    {
        //Debug.Log("Health" + currentHealth + "");
        healthSlider.value = currentHealth;
    }

    private void Death()
    {
        SpawnManager.lastLevelScene = SceneManager.GetActiveScene().name;

        // Reset the player's health for when they return (optional, depends on your game's design).

        // REMOVE AND REPLACE WITH ACTUAL data stuff later
        PlayerMovement movement = GetComponent<PlayerMovement>();
        movement.KillPlayer();



        // Load the GameOver scene.
        SceneManager.LoadScene(SpawnManager.lastLevelScene);
        Heal(MaxHealth);


    }

    public IEnumerator Die()
    {
        if (AnalyticsManager.Instance != null)
        {
            AnalyticsManager.Instance.PlayerDeathEvent();
        }
        else
        {
            Debug.LogError("AnalyticsManager instance not found");
        }
        this.animator.SetBool("Dead", true);
        this.GetComponent<PlayerMovement>().enabled = false;
        this.GetComponent<PlayerHealth2>().enabled = false;

        yield return new WaitForSeconds(1.5f); // TODO: Needs to be replaced with death animation
        Death();
    }


    public int getMaxHealth()
    {
        return MaxHealth;
    }
    public void setMaxHealth(int maxHealth)
    {
        MaxHealth = maxHealth;
    }
    public int getHealth()
    {
        return currentHealth;
    }
    public void setHealth(int health)
    {
        currentHealth = health;

    }
}
