using UnityEngine;
using System.Collections;

public class ParticleSystemManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] staticParticles; // Other particles
    [SerializeField] private float delayBeforeMove = 2f; // Delay before moving
    [SerializeField] private Transform spawnPoint;
    private ParticleSystem movingParticle;

    void StartParticles()
    {
        // Play all static particles
        foreach (var particle in staticParticles)
        {
            ParticleSystem newParticle = Instantiate(particle, spawnPoint.position, Quaternion.identity);
            if (particle == staticParticles[0]) movingParticle = newParticle;
            newParticle.Play();
        }

        StartCoroutine(StartEffect());
    }

    private IEnumerator StartEffect()
    {
        yield return new WaitForSeconds(delayBeforeMove);
        MovingParticle movieParticle= movingParticle.GetComponent<MovingParticle>();
        movieParticle.StartMoving();
    }
}
