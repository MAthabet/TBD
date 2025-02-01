using UnityEngine;
using System.Collections.Generic;

public class MovingParticle : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private ParticleSystem[] collideParticles;
    [SerializeField] private GameObject player;
    private ParticleSystem particleSystemComponent;
    private bool isMoving = false;

    void Start()
    {
        particleSystemComponent = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (isMoving && particleSystemComponent != null)
        {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("dds");
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
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("dsdfds");
        //if (other.contacts.Length > 0)
        //{
        //    Vector3 collisionPosition = other.gameObject.transform.position;
        //    foreach (var collideParticle in collideParticles)
        //    {
        //        ParticleSystem newParticle = Instantiate(collideParticle, collisionPosition, Quaternion.identity);
        //        newParticle.Play();
        //    }
        //    particleSystemComponent.Stop();
        //    Destroy(gameObject);
        //}
    }
    private void OnTriggerStay(Collider other)
    {
        Debug.Log("stay");
    }
    public void StartMoving()
    {
        isMoving = true;
    }

    //void OnParticleCollision(GameObject other)
    //{
    //    int numCollisionEvents = particleSystemComponent.GetCollisionEvents(other, collisionEvents);

    //    for (int i = 0; i < numCollisionEvents; i++)
    //    {
    //        Vector3 collisionPosition = collisionEvents[i].intersection;

    //        foreach (var collideParticle in collideParticles)
    //        {
    //            Instantiate(collideParticle, collisionPosition, Quaternion.identity);
    //        }
    //    }

    //    particleSystemComponent.Stop();
    //    Destroy(gameObject);
    //}
}
