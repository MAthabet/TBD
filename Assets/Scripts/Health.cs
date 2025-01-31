using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float Maxhealth = 100f;
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
        currentHealth -= damage;
        Debug.Log("currentHealt:" + currentHealth);
        if (currentHealth <= 0)
        {
            //Die();
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
        animator.SetTrigger("Death");
        Destroy(gameObject);
    }

}
