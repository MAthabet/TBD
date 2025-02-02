using UnityEngine;
using static PickUpsManager;

public class PlayerStats : MonoBehaviour
{
    public float maxHealth = 100;
    public float maxMagicCharge = 100;
    public float maxStamina = 100;

    public PickUps[] pickedUp = new PickUps[2];
    public float currentHealth = 100;
    public float currentMagicCharge;
    public bool isDead;

    [SerializeField] GameObject deathMenu;

    Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
        currentMagicCharge = 0;
        animator = GetComponent<Animator>();
        
    }

    public void IncreaseCurrentHealth(float health)
    {
        currentHealth += health;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
        UiManager.Instance.UpdatePlayerHealthBar();
    }
    public void onHit(float Damage)
    {
        currentHealth -= Damage;
        UiManager.Instance.UpdatePlayerHealthBar();
        if (currentHealth <= 0)
        {
            Die();
            animator.SetTrigger("Death");
        }
    }
    public void onCollectEssence()
    {
        currentMagicCharge += 10;
        UiManager.Instance.UpdateMagicCharge();
    }
    public void Die()
    {
        isDead = true;
        StartCoroutine(DeathRoutine());
    }


    private System.Collections.IEnumerator DeathRoutine()
    {
        animator.SetTrigger("Death");
        yield return new WaitForSeconds(2.5f);
        //game over
        deathMenu.SetActive(true);
    }
}
