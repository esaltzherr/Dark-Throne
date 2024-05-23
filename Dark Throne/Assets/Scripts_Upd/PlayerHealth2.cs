using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth2 : MonoBehaviour
{
    // public static int MaxHealth = 100;
    // private int currentHealth = MaxHealth;
    // private int healthItemsCollected = 0; // counter for health items collected
    //[SerializeField] private Slider healthSlider;
    [SerializeField] private Animator animator;
    [SerializeField] private Animator Portrait_animator;

    public static int MaxHearts = 6;
    public int currentHearts = MaxHearts;
    public Image[] hearts;

    private bool recentlyHealed = false;
    private int heartHealed;


    private void Start()
    {

        /*if (healthSlider == null)
        {
            healthSlider = GetComponent<Slider>();
            if (healthSlider == null)
            {
                Debug.LogError("Slider component not found on " + gameObject.name + ". Please attach a Slider component.", this);
            }
        }
        */
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

    public int getCurrentHearts()
    {
        return currentHearts;
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
        currentHearts -= damageAmount;
        Portrait_animator.SetInteger("Health", currentHearts);
        Debug.Log("Player took " + damageAmount + " damage. Current health: " + currentHearts);
        animator.SetTrigger("Is_Hit");
        Portrait_animator.SetTrigger("Portrait_hit");
        IEnumerator invuln = GetComponent<PlayerInvulnerability>().BecomeInvulnerable();
        StartCoroutine(invuln);
        if (currentHearts <= 0)
        {
            StartCoroutine(Die());
        }
    }

    public void Heal(int healAmount)
    {
        //currentHearts = Mathf.Min(currentHearts + healAmount, MaxHearts);
        if (currentHearts < MaxHearts)
        {
            currentHearts = Mathf.Min(currentHearts + healAmount, MaxHearts);
            Portrait_animator.SetInteger("Health", currentHearts);
            Portrait_animator.SetTrigger("Portrait_hit");
            heartHealed = currentHearts;
            recentlyHealed = true;
        }
        //StartCoroutine(disableHealAnimator(currentHearts));

        // Debug.Log("Player healed " + healAmount + " health. Current health: " + currentHealth);
    }


    private void UpdateHealthUI()
    {
        //Debug.Log("Health" + currentHealth + "");

        //healthSlider.value = currentHealth;
        for (int i = 0; i < MaxHearts; i++)
        {
            if (i < currentHearts)
            {
                hearts[i].gameObject.SetActive(true);
                //hearts[i].GetComponent<Animator>().enabled = true
            }
            else
            {
                hearts[i].gameObject.SetActive(false);
            }
        }
        if (recentlyHealed)
        {
            Debug.Log("RecentlyHealed = " + recentlyHealed);
            hearts[heartHealed - 1].GetComponent<Animator>().SetTrigger("Heal");
            //StartCoroutine(disableHealAnimator(i));
            recentlyHealed = false;
        }

    }

    private void Death()
    {
        SpawnManager.lastLevelScene = SceneManager.GetActiveScene().name;

        // Reset the player's health for when they return (optional, depends on your game's design).

        // REMOVE AND REPLACE WITH ACTUAL data stuff later
        // PlayerMovement movement = GetComponent<PlayerMovement>();
        // movement.KillPlayer();

        this.GetComponent<PlayerMovement>().enabled = true;
        this.GetComponent<PlayerHealth2>().enabled = true;
        this.animator.SetBool("Dead", false);

        // // Load the GameOver scene.
        // SceneManager.LoadScene(SpawnManager.lastLevelScene);

        // Commented out because it should heal you in SaveScript Resapwn
        Heal(MaxHearts);

        // Find the SaveLoadJSONCheckpoints script in the scene
        SaveLoadJSONPlayer saveLoadPlayerScript = FindObjectOfType<SaveLoadJSONPlayer>();
        if (saveLoadPlayerScript != null)
        {

            saveLoadPlayerScript.Respawn();
        }
        else
        {
            Debug.LogError("SaveLoadJSONPlayer script not found in the scene!");
        }

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


    public int getMaxHearts()
    {
        return MaxHearts;
    }
    public void setMaxHearts(int maxHearts)
    {
        MaxHearts = maxHearts;
    }
    // private void IncreaseMaxHealth(int amount)
    // {
    //     MaxHealth += amount; // increase max health
    //     Heal(amount); // heal the player by the amount increased
    //     UpdateHealthUI(); // update the health UI
    // }

    public int getHearts()
    {
        return currentHearts;
    }
    public void setHearts(int health)
    {
        currentHearts = health;

    }

    IEnumerator disableHealAnimator(int currentHP)
    {
        yield return new WaitForSeconds(0.267f);
        hearts[currentHP].GetComponent<Animator>().enabled = false;
    }


    // public void CollectHealthItem(int healthBonus)
    // {
    //     healthItemsCollected++; // increment health item count
    //     if (healthItemsCollected % 3 == 0) // check if this is the third item collected
    //     {
    //         IncreaseMaxHealth(healthBonus);
    //     }
    //     else
    //     {
    //         Heal(healthBonus); // heal normally if not the third item
    //     }
    // }
}
