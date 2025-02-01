using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiManager : MonoBehaviour
{
    private static UiManager instance;
    public static UiManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UiManager>();
                if (instance == null)
                {
                    GameObject go = new GameObject("PickUpsManager");
                    instance = go.AddComponent<UiManager>();
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    [SerializeField] private Slider playerHealthSlider;
    [SerializeField] private Slider bossHealthSlider;
    [SerializeField] private Slider magicChargeSlider;
    [SerializeField] private RawImage[] itemSlots;
    [SerializeField] private Texture2D[] images;

    private PlayerStats playerStats;
    private BossLogic bossLogic;

    void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        bossLogic = FindObjectOfType<BossLogic>();

        playerHealthSlider.maxValue = playerStats.maxHealth;
        magicChargeSlider.maxValue = playerStats.maxMagicCharge;
        bossHealthSlider.maxValue = bossLogic.MaxHP;
    }

    public void UpdatePlayerHealthBar()
    {
        playerHealthSlider.value = playerStats.currentHealth;
    }
    public void UpdateBossHealthBar()
    {
        bossHealthSlider.value = bossLogic.currentHP;
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
