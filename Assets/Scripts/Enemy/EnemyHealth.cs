using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public bool isdeadcheck = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            Debug.Log("Enemy touches player");
            isdeadcheck = true;

        }
    }
}
