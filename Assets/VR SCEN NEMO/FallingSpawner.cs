using UnityEngine;

public class FallingSpawner : MonoBehaviour
{
    public string[] spawnTags = { "Collectible", "Hazard" };
    public float spawnRangeX = 2f;

    [Header("Spawn Timing")]
    public float minSpawnInterval = 0.5f;
    public float maxSpawnInterval = 1.2f;

    [Header("Multi-Spawn Settings")]
    public int minObjectsPerSpawn = 1;
    public int maxObjectsPerSpawn = 3;

    void Start()
    {
        ScheduleNextSpawn();
    }

    void ScheduleNextSpawn()
    {
        float interval = Random.Range(minSpawnInterval, maxSpawnInterval);
        Invoke(nameof(Spawn), interval);
    }

    void Spawn()
    {
        int count = Random.Range(minObjectsPerSpawn, maxObjectsPerSpawn + 1);
        for (int i = 0; i < count; i++)
        {
            string tag = spawnTags[Random.Range(0, spawnTags.Length)];
            float x = Random.Range(-spawnRangeX, spawnRangeX);
            Vector3 pos = new Vector3(x, transform.position.y, transform.position.z);
            ObjectPool.Instance.Spawn(tag, pos, Quaternion.identity);
        }

        ScheduleNextSpawn();
    }
}