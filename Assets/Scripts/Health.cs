using UnityEngine;

public class Health : MonoBehaviour
{
    public float Maxhealth = 100f;
    public float currentHealth;
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

    public void Die()
    {
        animator.SetTrigger("Death");
        Destroy(gameObject);
    }
}
