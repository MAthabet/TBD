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
