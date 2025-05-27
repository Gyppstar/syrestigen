using UnityEngine;

public class FallingSpawner : MonoBehaviour
{
    public float spawnInterval = 1f;
    public float spawnRangeX = 2f;
    public string[] spawnTags = { "Collectible", "Hazard" };

    void Start()
    {
        InvokeRepeating(nameof(Spawn), 1f, spawnInterval);
    }

    void Spawn()
    {
        string tag = spawnTags[Random.Range(0, spawnTags.Length)];
        float x = Random.Range(-spawnRangeX, spawnRangeX);
        Vector3 pos = new Vector3(x, transform.position.y, transform.position.z);
        ObjectPool.Instance.Spawn(tag, pos, Quaternion.identity);
    }
}