using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Splines;

public class SpawningManager : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private SplineContainer spline;
    [SerializeField] private float throwSpeed = 10f;
    [SerializeField] private float throwIntervals = 3f;
    [SerializeField] private Transform player;
    [SerializeField] private float Range = 0.1f;

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
        float playerT = GetNearestPointOnSpline(player.position);
        int randomEnemyIndex = Random.Range(0, enemyPrefabs.Length);
        float randomSplinePosition = playerT + Random.Range(-Range, Range);
        randomSplinePosition = Mathf.Clamp(randomSplinePosition, 0.05f, 0.98f);
        Vector3 targetPosition = spline.EvaluatePosition(randomSplinePosition);
        GameObject enemy = Instantiate(enemyPrefabs[randomEnemyIndex], transform.position, Quaternion.identity);
        targetPosition.y += enemy.GetComponent<CharacterController>().height/2;
        Vector3 throwDirection = (targetPosition - transform.position).normalized;
        StartCoroutine(MoveEnemyAlongTrajectory(enemy, throwDirection, targetPosition));
    }
    private float GetNearestPointOnSpline(Vector3 position)
    {
        float nearestT = 0f;
        float nearestDistance = float.MaxValue;

        for (float t = 0f; t <= 1f; t += 0.01f)
        {
            Vector3 splinePoint = spline.EvaluatePosition(t);
            float distance = Vector3.Distance(position, splinePoint);
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestT = t;
            }
        }

        return nearestT;
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

                if (Vector3.Distance(enemy.transform.position, target) < 0.3f)
                {
                    EnemyLogic enemyLogic = enemy.GetComponent<EnemyLogic>();
                    enemyLogic.enabled = true;
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
