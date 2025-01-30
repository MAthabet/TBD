using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100;
    private float currentHealth;
    [SerializeField] private float maxMagicCharge = 100;
    private float currentMagicCharge;
    [SerializeField] private float maxStamina = 100;
    private float currentStamina;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
        currentMagicCharge = 0;
        currentStamina = maxStamina;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void onHit(float Damage)
    {
        currentHealth =-Damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    void onCollectEssence()
    {
        currentMagicCharge += 10;
    }
    void Die()
    {

    }
}
