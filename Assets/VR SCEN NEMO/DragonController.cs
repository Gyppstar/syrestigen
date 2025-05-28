using UnityEngine;
using UnityEngine.InputSystem;

public class DragonController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 2f;
    public float leftLimit = -2f;
    public float rightLimit = 2f;

    [Header("Rotation Settings")]
    public float rotationSpeed = 10f;
    public float idleRotationDelay = 2f;

    private Vector2 input;
    private float lastInputTime;

    void Update()
    {
        // Get input from gamepad or keyboard
        input = Gamepad.current != null ? Gamepad.current.leftStick.ReadValue() :
                (Keyboard.current != null ? new Vector2(
                    (Keyboard.current.aKey.isPressed ? -1 : 0) + (Keyboard.current.dKey.isPressed ? 1 : 0),
                    0) : Vector2.zero);

        // Movement
        if (Mathf.Abs(input.x) > 0.01f)
        {
            float newX = Mathf.Clamp(transform.position.x + input.x * moveSpeed * Time.deltaTime, leftLimit, rightLimit);
            transform.position = new Vector3(newX, transform.position.y, transform.position.z);
            lastInputTime = Time.time;

            // Rotate left/right
            float targetY = input.x < 0 ? 270f : 90f;
            Quaternion targetRotation = Quaternion.Euler(0f, targetY, 0f);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
        else
        {
            // Rotate back to forward-facing (Y = 180) after idle time
            if (Time.time - lastInputTime >= idleRotationDelay)
            {
                Quaternion targetRotation = Quaternion.Euler(0f, 180f, 0f);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectible"))
        {
            GameManager.instance.AddScore(100);
            other.gameObject.SetActive(false);
        }
        else if (other.CompareTag("Hazard"))
        {
            GameManager.instance.TakeDamage(1);
            other.gameObject.SetActive(false);
        }
    }
}