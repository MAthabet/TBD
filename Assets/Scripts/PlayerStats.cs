using UnityEngine;
using static PickUpsManager;

public class PlayerStats : MonoBehaviour
{
    public float maxHealth = 100;
    public float maxMagicCharge = 100;
    public float maxStamina = 100;

    public PickUps[] pickedUp = new PickUps[2];
    public float currentHealth;
    public float currentMagicCharge;
    private float currentStamina;

    Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
        currentMagicCharge = 0;
        currentStamina = maxStamina;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void IncreaseCurrentHealth(float health)
    {
        currentHealth += health;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
    }
    public void onHit(float Damage)
    {
        currentHealth -= Damage;
       // UiManager.Instance.UpdatePlayerHealthBar();
        //Debug.Log("health :"+ currentHealth);
        if (currentHealth <= 0)
        {
            Die();
            animator.SetTrigger("Death");
        }
    }
    void onCollectEssence()
    {
        currentMagicCharge += 10;
    }
    public void Die()
    {
        StartCoroutine(DeathRoutine());
    }


    private System.Collections.IEnumerator DeathRoutine()
    {
        animator.SetTrigger("Death");
        yield return new WaitForSeconds(2.5f);

        Destroy(gameObject);
    }
}
