using UnityEngine;

public class Wepon : MonoBehaviour
{
    public float damage = 100f;
    public string layerToHit;
    public bool test = false;
    public Health myHealth;

    public void Start()
    {
        myHealth = this.transform.parent.parent.parent.parent.parent. parent.parent.parent.GetComponent<Health>();
    }

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
                    other.GetComponent<PlayerPowerUpCollector>().disactive();
                    myHealth.TakeDamage(damage);

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
