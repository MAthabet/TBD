using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiManager : MonoBehaviour
{
    [SerializeField] private Slider playerHealthSlider;
    [SerializeField] private Slider bossHealthSlider;
    [SerializeField] private Slider magicChargeSlider;
    [SerializeField] private RawImage[] itemSlots;
    [SerializeField] private Texture2D[] images;
    [SerializeField] private float bossMaxHP;

    private PlayerStats playerStats;

    void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();

        playerHealthSlider.maxValue = playerStats.maxHealth;
        magicChargeSlider.maxValue = playerStats.maxMagicCharge;
        bossHealthSlider.maxValue = bossMaxHP;
    }

    public void UpdatePlayerHealthBar()
    {
        playerHealthSlider.value = playerStats.currentHealth;
    }
    public void UpdateBossHealthBar(float hp)
    {
        bossHealthSlider.value = hp;
    }

    public void UpdateMagicCharge()
    {
        magicChargeSlider.value = playerStats.currentMagicCharge;
    }

    public void UpdateInventoryDisplay()
    {
        for (int i = 0; i < playerStats.pickedUp.Length; i++)
        {
            itemSlots[i].texture = images[(int)playerStats.pickedUp[i]];
        }
    }
}
