using UnityEngine;

public class FallingObject : MonoBehaviour
{
    [Header("Falling Settings")]
    public float fallSpeed = 2f;

    [Header("Despawn Settings")]
    public float despawnY = -5f;

    [Header("Lock Settings")]
    public float lockZ = 17f;

    void Update()
    {
        // Move downward
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);

        // Lock Z position
        Vector3 pos = transform.position;
        pos.z = lockZ;
        transform.position = pos;

        // Despawn if too low
        if (transform.position.y < despawnY)
            gameObject.SetActive(false);
    }
}