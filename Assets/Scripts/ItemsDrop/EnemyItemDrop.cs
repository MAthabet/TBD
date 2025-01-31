using UnityEngine;
using UnityEngine.UIElements;

public class EnemyItemDrop : MonoBehaviour
{
    public GameObject[] itemList;
    private int itemIndex;
    private int totalItemsInArray = 0;
    private Transform enemyPos;

    private Health EnemyHealth;
    void Start()
    {
        EnemyHealth = GetComponent<Health>();

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

        if (EnemyHealth.currentHealth <= 0)
        {
            Debug.Log("alooooo");
            DropItem();
            EnemyHealth.Die();
            //Destroy(gameObject);
        }
    }

    void DropItem()
    {
        //enemy health script.isdeadcheck = false;
        //EnemyHealth.currentHealth = false;
        enemyPos = GetComponent<Transform>().transform.GetChild(0).transform;
        Instantiate(itemList[itemIndex], enemyPos.position, Quaternion.identity);
    }
}
