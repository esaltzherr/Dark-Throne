using System.Collections;
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

    
    public void ChangeHealth(int num)
    {
        if (!(GetComponent<PlayerInvulnerability>().isInvulnerable))
        {
            currentHealth += num;
            healthSlider.value = currentHealth;
            animator.SetTrigger("Is_Hit");
            IEnumerator invuln = GetComponent<PlayerInvulnerability>().BecomeInvulnerable();
            StartCoroutine(invuln);
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
