using UnityEngine;

public class PickUpsManager : MonoBehaviour
{
    private static PickUpsManager instance;
    public static PickUpsManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PickUpsManager>();
                if (instance == null)
                {
                    GameObject go = new GameObject("PickUpsManager");
                    instance = go.AddComponent<PickUpsManager>();
                }
            }
            return instance;
        }
    }

    public EnemyItemDrop enemyItemDrop;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        enemyItemDrop = FindObjectOfType<EnemyItemDrop>();
    }
    private void Start()
    {
        //enemyItemDrop = FindObjectOfType<EnemyItemDrop>();
    }

    public enum PickUps
    {
        none,
        Rum,
        Pills,
        Shield,
        Spike
    }
}
