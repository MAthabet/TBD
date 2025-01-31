using UnityEngine;

public class Health : MonoBehaviour
{
    public float Maxhealth = 100f;
    public float currentHealth;
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
            Debug.Log("currentHealt:"+ currentHealth);
        if (currentHealth <= 0)
        {
            //Die();
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = Maxhealth;

    }
    public void Die()
    {
        Destroy(gameObject);
    }
}
