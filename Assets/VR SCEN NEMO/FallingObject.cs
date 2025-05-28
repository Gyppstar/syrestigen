using UnityEngine;

public class FallingObject : MonoBehaviour
{
    [Header("Despawn Settings")]
    public float despawnY = -8f;

    [Header("Lock Settings")]
    public float lockZ = 17f;

    void Update()
    {
        // Use global fall speed from GameManager
        float fallSpeed = GameManager.instance.currentFallSpeed;

        // Fall movement
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