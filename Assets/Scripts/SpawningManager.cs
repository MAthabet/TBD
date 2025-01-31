using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Splines;

public class SpawningManager : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private SplineContainer spline;
    [SerializeField] private float throwSpeed = 10f;
    [SerializeField] private float throwIntervals = 3f;

    private float nextSpawnTime;
    private float spawnDelay;

    private void Start()
    {
        SetNextSpawnTime();
    }

    private void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            SetNextSpawnTime();
        }
    }

    private void SpawnEnemy()
    {
        int randomEnemyIndex = Random.Range(0, enemyPrefabs.Length);
        float randomSplinePosition = Random.Range(0f, 1f);
        Vector3 targetPosition = spline.EvaluatePosition(randomSplinePosition);
        GameObject enemy = Instantiate(enemyPrefabs[randomEnemyIndex], transform.position, Quaternion.identity);
        targetPosition.y += enemy.GetComponent<CharacterController>().height/2;
        Vector3 throwDirection = (targetPosition - transform.position).normalized;
        StartCoroutine(MoveEnemyAlongTrajectory(enemy, throwDirection, targetPosition));
    }

    private System.Collections.IEnumerator MoveEnemyAlongTrajectory(GameObject enemy, Vector3 direction, Vector3 target)
    {
        float time = 0;
        Vector3 startPos = enemy.transform.position;
        Vector3 initialVelocity = direction * throwSpeed;
        bool reachedTarget = false;

        while (!reachedTarget)
        {
            if (enemy != null)
            {
                enemy.transform.position = startPos + initialVelocity * time;

                if (Vector3.Distance(enemy.transform.position, target) < 0.1f)
                {
                    reachedTarget = true;
                }

                time += Time.deltaTime;
                yield return null;
            }
        }
    }

    private void SetNextSpawnTime()
    {
        spawnDelay = Random.Range(throwIntervals, throwIntervals+1.5f);
        nextSpawnTime = Time.time + spawnDelay;
    }
}
