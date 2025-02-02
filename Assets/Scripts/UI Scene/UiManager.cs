using UnityEngine;
using UnityEngine.UI;

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

    [SerializeField] GameObject player;
    [SerializeField] GameObject boss;
    private PlayerStats playerStats;
    private BossLogic bossLogic;

    void Start()
    {
        playerStats = player.GetComponent<PlayerStats>();
        bossLogic = boss.GetComponent<BossLogic>();

        playerHealthSlider.maxValue =100;
        playerHealthSlider.value = 100;
        magicChargeSlider.maxValue = playerStats.maxMagicCharge;
        bossHealthSlider.maxValue = bossLogic.MaxHP;
        UpdateBossHealthBar();
        UpdatePlayerHealthBar();
        UpdateMagicCharge();
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
        Debug.Log("Magic Charge: " + magicChargeSlider.value);
    }

}
