using Unity.VisualScripting;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [Header("Power-Up Settings")]
    [SerializeField] private string playerTag = "Player"; 
    [SerializeField] private PowerUpType powerUpType;
    
    
    public enum PowerUpType
    {
        HealthBoost,
        PowerUpBoost,
        Shield,
        SpikeArmor
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            ApplyPowerUpEffect(powerUpType , other);

            Destroy(gameObject); 
        }
    }

    private void ApplyPowerUpEffect(PowerUpType type , Collider player)
    {
        PlayerPowerUpCollector collector = player.GetComponent<PlayerPowerUpCollector>();
                PlayerStats playerHealth = player.GetComponent<PlayerStats>();
        switch (type)
        {
            case PowerUpType.HealthBoost:
                //make logic
                playerHealth.IncreaseCurrentHealth(10);
                break;

            case PowerUpType.PowerUpBoost:
                //make logic
                playerHealth.onCollectEssence();
                //collector.CollectPowerUp();
                break;

            case PowerUpType.Shield:
                //make logic
                collector.ActivateShield(PowerUpType.Shield);
                //add can't take damage

                break;

            case PowerUpType.SpikeArmor:
                //make logic
                collector.ActivateShield(PowerUpType.SpikeArmor);
                break;

        }

    }

   
}