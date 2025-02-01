using System.Collections;
using UnityEngine;

public class testVFX : MonoBehaviour
{
    [SerializeField] private MovingParticle movingParticlePrefab;
    [SerializeField] private ParticleSystem[] staticParticles;
    [SerializeField] private float delayBeforeMove = 2f;

    void Start()
    {
       // StartParticles();
    }

    void StartParticles()
    {
        foreach (var particle in staticParticles)
        {
            particle.Play();
        }

        StartCoroutine(StartEffect());
    }

    private IEnumerator StartEffect()
    {
        yield return new WaitForSeconds(delayBeforeMove);

        // Instantiate the moving particle and start it
        MovingParticle movingParticle = Instantiate(movingParticlePrefab, transform.position, Quaternion.identity);
        movingParticle.StartMoving();
    }
}