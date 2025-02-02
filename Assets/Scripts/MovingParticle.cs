using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using TMPro;

public class MovingParticle : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private ParticleSystem[] collideParticles;
    [SerializeField] private GameObject player;
    private ParticleSystem particleSystemComponent;
    private bool isMoving = false;
    [SerializeField] private float damage = 30;
    [SerializeField] GameObject boss;
    void Start()
    {
        particleSystemComponent = GetComponent<ParticleSystem>();
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    void Update()
    {
        if (isMoving && particleSystemComponent != null && player != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, boss.transform.position, moveSpeed * Time.deltaTime);

            transform.LookAt(boss.transform.position);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Boss"))
        {
            collision.gameObject.GetComponent<BossLogic>().TakeDamage(damage);
        }
        if (collision.contacts.Length > 0)
        {
            Vector3 collisionPosition = collision.contacts[0].point;
            foreach (var collideParticle in collideParticles)
            {
                ParticleSystem newParticle = Instantiate(collideParticle, collisionPosition, Quaternion.identity);
                newParticle.transform.LookAt(player.transform.position);
                newParticle.Play();
            }
            particleSystemComponent.Stop();
            Destroy(gameObject);
        }
    }
  
    public void StartMoving()
    {
        isMoving = true;
    }

  
}
