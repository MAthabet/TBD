using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float Maxhealth = 100f;
    bool dead = false;
    private float currentHealth;
    Animator animator;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = Maxhealth;
        animator = GetComponent<Animator>();
    }
    public void TakeDamage(float damage)
    {
        if (dead) return;
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
            dead = true;
        }
        else animator.SetTrigger("Hit");
    }
    public void IncreaseCurrentHealth(float health)
    {
        currentHealth+= health;
        if(currentHealth > Maxhealth) currentHealth = Maxhealth;
    }
    public float GetCurrentHealth()
    {
        return currentHealth;
    }
    public void Die()
    {        
        StartCoroutine(DeathRoutine());
    }
    

    private System.Collections.IEnumerator DeathRoutine()
    {
        animator.SetTrigger("Death");
        PickUpsManager.Instance.enemyItemDrop.DropItem(transform);
        yield return new WaitForSeconds(2.5f);

        Destroy(gameObject);
    }

}
