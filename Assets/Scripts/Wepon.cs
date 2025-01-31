using UnityEngine;

public class Wepon : MonoBehaviour
{
    public float damage = 100f;
    public string layerToHit;
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(layerToHit))
        {
            Debug.Log("Hit");
            other.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
