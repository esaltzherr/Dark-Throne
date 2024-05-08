using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public static int MaxHealth = 100;
    private int currentHealth = MaxHealth;
    private int healthItemsCollected = 0; // counter for health items collected

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
                Debug.LogError("Animator component not found on " + gameObject.name + ". Please attach an Animator component.", this);
            }
        }
    }

    public void CollectHealthItem(int healthBonus)
    {
        healthItemsCollected++; // increment health item count
        if (healthItemsCollected % 3 == 0) // check if this is the third item collected
        {
            IncreaseMaxHealth(healthBonus);
        }
        else
        {
            Heal(healthBonus); // heal normally if not the third item
        }
    }

    private void IncreaseMaxHealth(int amount)
    {
        MaxHealth += amount; // increase max health
        Heal(amount); // heal the player by the amount increased
        UpdateHealthUI(); // update the health UI
    }

    public void Heal(int healAmount)
    {
        currentHealth = Mathf.Min(currentHealth + healAmount, MaxHealth); // ensure current health does not exceed max health
        UpdateHealthUI(); // update the health UI
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount; // reduce current health by damage amount
        animator.SetTrigger("Is_Hit"); // play hit animation
        IEnumerator invuln = GetComponent<PlayerInvulnerability>().BecomeInvulnerable();
        StartCoroutine(invuln); // Start invulnerability coroutine
        UpdateHealthUI(); // Update the health UI
        if (currentHealth <= 0)
        {
            StartCoroutine(Die()); // Start death coroutine if health falls below or equals zero
        }
    }

    private void UpdateHealthUI()
    {
        healthSlider.value = currentHealth; // Set the slider value to current health
    }

    private void Death()
    {
        SceneManager.LoadScene(SpawnManager.lastLevelScene); // Reload the last level scene
        Heal(MaxHealth); // Reset health to max
    }

    public IEnumerator Die()
    {
        yield return new WaitForSeconds(1); // Delay for death animation
        Death();
    }
}

