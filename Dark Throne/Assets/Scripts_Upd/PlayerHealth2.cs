using UnityEngine;
using UnityEngine.UI;

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



        UpdateHealthUI();
    }

    public void ChangeHealth(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, MaxHealth);
        UpdateHealthUI();

        if (amount < 0)
        {
            animator.SetTrigger("Hurt");
            // Optional: Add invulnerability period or effects
        }

        if (currentHealth <= 0)
        {
            // Handle player death, e.g., restart level or show game over screen
            Death();
        }
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        Debug.Log("Player took " + damageAmount + " damage. Current health: " + currentHealth);
    }

    public void Heal(int healAmount)
    {
        currentHealth = Mathf.Min(currentHealth + healAmount, MaxHealth);
        Debug.Log("Player healed " + healAmount + " health. Current health: " + currentHealth);
    }


    private void UpdateHealthUI()
    {
        healthSlider.value = currentHealth;
    }

    private void Death(){
        Debug.Log("Player has died");
    }
}
