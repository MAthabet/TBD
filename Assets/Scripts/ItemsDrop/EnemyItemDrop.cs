using UnityEngine;
using UnityEngine.UIElements;

public class EnemyItemDrop : MonoBehaviour
{
    public GameObject[] itemList;
    private int itemIndex;
    private int totalItemsInArray = 0;
    private Transform enemyPos;

    private EnemyHealth EnemyHealth;
    void Start()
    {
        EnemyHealth = GetComponent<EnemyHealth>();

        foreach(GameObject item in itemList)
        {
            totalItemsInArray++;
        }
        // 4 items = 25% drop rate of all items
        itemIndex = Random.Range(0, totalItemsInArray);
    }

    void Update()
    {
        //if (enemy health script.isdeadcheck == true){

        //   DropItem();
        //}

        if (EnemyHealth.isdeadcheck == true)
        {
            Debug.Log("alooooo");
            DropItem();
            Destroy(gameObject);
        }
    }

    void DropItem()
    {
        //enemy health script.isdeadcheck = false;
        EnemyHealth.isdeadcheck = false;
        enemyPos = GetComponent<Transform>().transform.GetChild(0).transform;
        Instantiate(itemList[itemIndex], enemyPos.position, Quaternion.identity);
    }
}
