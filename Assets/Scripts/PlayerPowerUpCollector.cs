using UnityEngine;
using UnityEngine.UI;
using static PowerUp;

public class PlayerPowerUpCollector : MonoBehaviour
{
    [Header("Power-Up Settings")]
    [SerializeField] private int maxPowerUps = 10;
    [SerializeField] private Slider powerUpSlider;
    [Header("Shield Settings")]
    [SerializeField] private GameObject shieldObject;
    [SerializeField] private float shieldTime = 5f;

    [Header("SpikeArmor Settings")]
    [SerializeField] private GameObject SpikeArmorObject;
    [SerializeField] private float SpikeArmorTime = 5f;
    private int currentPowerUps = 0;
    bool pick = false;
    public bool getSpikeArmor = false;
    public bool getShield = false;
    private void Start()
    {
        if (powerUpSlider != null)
        {
            powerUpSlider.minValue = 0;
            powerUpSlider.maxValue = maxPowerUps;
            powerUpSlider.value = currentPowerUps;
        }
    }

    public void CollectPowerUp()
    {
        if (currentPowerUps < maxPowerUps)
        {
            currentPowerUps++;
            UpdateSlider();
        }
        
    }

    private void UpdateSlider()
    {
        if (powerUpSlider != null)
        {
            powerUpSlider.value = currentPowerUps;
        }
    }
    public void ActivateShield(PowerUpType type)
    {
        if (type == PowerUpType.Shield) getSpikeArmor = true;
        if (type == PowerUpType.SpikeArmor) getShield = true;
        if (pick)
        {
            if (type == PowerUpType.Shield || type == PowerUpType.SpikeArmor)
            {
                StopAllCoroutines();

                shieldObject.SetActive(false);
                SpikeArmorObject.SetActive(false);
            }
            pick = false;
        }
        
        StartCoroutine(ShieldRoutine(type));
        
    }
    public void disactive()
    {
        //StopAllCoroutines();
        getShield = false;
        getSpikeArmor = false;
        shieldObject.SetActive(false);
        SpikeArmorObject.SetActive(false);
    }

    private System.Collections.IEnumerator ShieldRoutine(PowerUpType type)
    {
        pick = true;
        GameObject effect = null;
        float time = 0;
        if(type == PowerUpType.Shield)
        {
            effect = shieldObject;
            time = shieldTime;
        }
        else if(type == PowerUpType.SpikeArmor){
            effect = SpikeArmorObject;
            time = SpikeArmorTime;
        }
        // Activate the shield
        if (effect != null)
        {
            effect.SetActive(true);
        }

        Debug.Log("Shield activated!");

        yield return new WaitForSeconds(time);

        if (effect != null)
        {
            effect.SetActive(false);
        }
        getShield = false;
        getSpikeArmor = false;
        Debug.Log("Shield deactivated!");
    }
}