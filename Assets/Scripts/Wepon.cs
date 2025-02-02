using UnityEngine;

public class Wepon : MonoBehaviour
{
    public float damage = 100f;
    public string layerToHit;
    public bool test = false;
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(layerToHit))
        {
            
            if (layerToHit == "Player")
            {
                if (other.GetComponent<PlayerPowerUpCollector>().getShield)
                {
                    other.GetComponent<PlayerPowerUpCollector>().getShield = false;
                    return;
                }
                if (other.GetComponent<PlayerPowerUpCollector>().getSpikeArmor)
                {
                    other.GetComponent<PlayerPowerUpCollector>().getSpikeArmor = false;
                    GetComponent<Health>().TakeDamage(damage);
                    return;
                }
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
