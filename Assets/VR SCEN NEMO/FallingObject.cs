using UnityEngine;

public class FallingObject : MonoBehaviour
{
    [Header("Settings")]
    public float fallSpeed = 1.5f; // Local speed per object

    private void Update()
    {
        // Move object downward at constant speed
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);

        // Lock Z position to stay on gameplay plane
        Vector3 pos = transform.position;
        pos.z = 17f;
        transform.position = pos;

        // Despawn when below screen
        if (transform.position.y < -8f)
        {
            gameObject.SetActive(false);
        }
    }
}