using UnityEngine;

public class Wepon : MonoBehaviour
{
    public float damage = 100f;
    public string layerToHit;
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(layerToHit))
        {
            if (layerToHit == "Player")
            {
                other.GetComponent<PlayerStats>().onHit(damage);
            }
            else
            {

            Debug.Log("Hit");
                other.GetComponent<Health>().TakeDamage(damage);
            }
            
        }
    }
}
