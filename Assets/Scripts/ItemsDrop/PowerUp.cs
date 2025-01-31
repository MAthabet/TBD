using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [Header("Power-Up Settings")]
    [SerializeField] private string playerTag = "Player"; 
    [SerializeField] private PowerUpType powerUpType; 

    private enum PowerUpType
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
            Debug.Log("hi");
            ApplyPowerUpEffect(powerUpType);

            Destroy(gameObject); 
        }
    }

    private void ApplyPowerUpEffect(PowerUpType type)
    {
        switch (type)
        {
            case PowerUpType.HealthBoost:
                //make logic
                break;

            case PowerUpType.PowerUpBoost:
                //make logic
                break;

            case PowerUpType.Shield:
                //make logic
                break;

            case PowerUpType.SpikeArmor:
                //make logic
                break;

        }

    }

   
}