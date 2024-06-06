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
            TakeDamage(40);
        }
        if (Input.GetKeyDown(SpawnHP))
        {
            Debug.Log("fuck");
            bossCanvas.gameObject.SetActive(true);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        //healthSlider.value = currentHealth;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // add boss death logic here
        Debug.Log("Boss died!");
    }
}

