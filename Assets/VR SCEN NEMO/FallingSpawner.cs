using UnityEngine;

public class FallingSpawner : MonoBehaviour
{
    [Header("Spawn Position")]
    public float spawnRangeX = 2f;

    [Header("Spawn Timing")]
    public float startInterval = 2.0f;
    public float minInterval = 0.5f;
    public float intervalDecreaseRate = 0.01f; // per second

    [Header("Hazard Spawn Ratio")]
    public float startingHazardChance = 0.2f;
    public float maxHazardChance = 0.8f;
    public float hazardRampRate = 0.01f; // per second

    private float currentInterval;
    private float currentHazardChance;
    private float spawnTimer;

    void Start()
    {
        currentInterval = startInterval;
        currentHazardChance = startingHazardChance;
    }

    void Update()
    {
        // Gradually decrease interval
        if (currentInterval > minInterval)
            currentInterval -= intervalDecreaseRate * Time.deltaTime;

        // Gradually increase hazard chance
        if (currentHazardChance < maxHazardChance)
            currentHazardChance += hazardRampRate * Time.deltaTime;

        // Count time and spawn when timer exceeds current interval
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= currentInterval)
        {
            spawnTimer = 0f;
            Spawn();
        }
    }

    void Spawn()
    {
        string tag = Random.value < currentHazardChance ? "Hazard" : "Collectible";
        float x = Random.Range(-spawnRangeX, spawnRangeX);
        Vector3 pos = new Vector3(x, transform.position.y, transform.position.z);

        ObjectPool.Instance.Spawn(tag, pos, Quaternion.identity);
    }
}