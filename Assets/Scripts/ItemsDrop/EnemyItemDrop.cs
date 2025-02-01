using UnityEngine;
using UnityEngine.UIElements;

public class EnemyItemDrop : MonoBehaviour
{
    public GameObject[] itemList;
    private int itemIndex;
    private int totalItemsInArray = 0;
    //private Transform enemyPos;
    //private Animator animator;
    //private Health EnemyHealth;
    void Start()
    {
        //EnemyHealth = GetComponent<Health>();
        //animator = GetComponent<Animator>();

        totalItemsInArray = itemList.Length;
        // 4 items = 25% drop rate of all items
        itemIndex = Random.Range(0, totalItemsInArray);
    }   
    
    public void DropItem(Transform deadEnemyPos)
    {
        //enemy health script.isdeadcheck = false;
        //EnemyHealth.currentHealth = false;
        //enemyPos = GetComponent<Transform>().transform.GetChild(0).transform;

        itemIndex = Random.Range(0, totalItemsInArray);
        Instantiate(itemList[itemIndex], deadEnemyPos.position, Quaternion.identity);
    }
}
